using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;


namespace Identity.Controllers
{

    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        // this is the 455 implementation part 
        private IProductRepository _repo;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        //private readonly ItemRecommendation.ProductService _recommendationService;
        public AdminController(IProductRepository temp, IHostEnvironment hostEnvironment, UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash)
        {
            _repo = temp;
            // These are for ReviewOrders
            _onnxModelPath = System.IO.Path.Combine(hostEnvironment.ContentRootPath, "decision_tree_classifier.onnx");
            //initialize the InferenceSession
            _session = new InferenceSession(_onnxModelPath);
            // This is for Item Recommanmdations
            //_recommendationService = new ItemRecommendation.ProductService();
            userManager = usrMgr;
            passwordHasher = passwordHash;

        }

        //private UserManager<AppUser> userManager;
        //private IPasswordHasher<AppUser> passwordHasher;

        //public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash)
        //{
        //    userManager = usrMgr;
        //    passwordHasher = passwordHash;
        //}

        //public IActionResult FraudPrediction()
        //{
        //    var records = _repo.Orders.OrderByDescending(o => o.date).Take(100).ToList();
        //    var predictions = new List<FraudPrediction>();
        //    var input = new List<float>(new float[393]); // Ensure the list has 393 features initialized to 0

        //    foreach (var record in records)
        //    {
        //        // Initialize or reset the input list for each record
        //        for (int i = 0; i < 393; i++)
        //            input[i] = 0;  // Reset or other logic to set default/placeholder values

        //        // Populate the input list with actual values from the record
        //        input[0] = (float)record.customer_ID;
        //        input[1] = (float)record.time;
        //        // continue for other features...

        //        var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, 393 });
        //        var inputs = new List<NamedOnnxValue>
        //{
        //    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
        //};

        //        using (var results = _session.Run(inputs))
        //        {
        //            var prediction = results.FirstOrDefault()?.AsTensor<long>().ToArray();
        //            var predictionResult = prediction != null && prediction.Length > 0 ? prediction[0].ToString() : "Error in prediction";
        //            predictions.Add(new FraudPrediction { Order = record, Prediction = predictionResult });
        //        }
        //    }

        //    return View(predictions);
        //}


        public IActionResult FraudPrediction()
        {
            var records = _repo.Orders
                .OrderByDescending(o => o.date)
                .Take(1000)
                .ToList();  // Fetch the 20 most recent records
            var predictions = new List<FraudPrediction>();  // Your ViewModel for the view
                                                            // Dictionary mapping the numeric prediction to an animal type
            var class_type_dict = new Dictionary<int, string>
            {
                { 0, "Not Fraud" },
                { 1, "Fraud" }
            };
            foreach (var record in records)
            {
                // Calculate days since January 1, 2022
                var january1_2022 = new DateTime(2022, 1, 1);
                var daysSinceJan12022 = record.date.HasValue ? Math.Abs((record.date.Value - january1_2022).Days) : 0;
                // Preprocess features to make them compatible with the model
                var input = new List<float>
                {
                    (float)record.transaction_ID,
                    (float)record.customer_ID,
                    (float)record.time,    
                    // fix amount if it's null
                    (float)(record.amount ?? 0),
                    // fix date
                    daysSinceJan12022,
                    // Check the Dummy Coded Data
                    record.day_of_week == "Mon" ? 1 : 0,
                    record.day_of_week == "Sat" ? 1 : 0,
                    record.day_of_week == "Sun" ? 1 : 0,
                    record.day_of_week == "Thu" ? 1 : 0,
                    record.day_of_week == "Tue" ? 1 : 0,
                    record.day_of_week == "Wed" ? 1 : 0,
                    record.entry_mode == "Pin" ? 1 : 0,
                    record.entry_mode == "Tap" ? 1 : 0,
                    record.type_of_transaction == "Online" ? 1 : 0,
                    record.type_of_transaction == "POS" ? 1 : 0,
                    record.country_of_transaction == "India" ? 1 : 0,
                    record.country_of_transaction == "Russia" ? 1 : 0,
                    record.country_of_transaction == "USA" ? 1 : 0,
                    record.country_of_transaction == "UnitedKingdom" ? 1 : 0,
                    // Use CountryOfTransaction if ShippingAddress is null
                    (record.shipping_address ?? record.country_of_transaction) == "India" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "Russia" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "USA" ? 1 : 0,
                    (record.shipping_address ?? record.country_of_transaction) == "UnitedKingdom" ? 1 : 0,
                    record.bank == "HSBC" ? 1 : 0,
                    record.bank == "Halifax" ? 1 : 0,
                    record.bank == "Lloyds" ? 1 : 0,
                    record.bank == "Metro" ? 1 : 0,
                    record.bank == "Monzo" ? 1 : 0,
                    record.bank == "RBS" ? 1 : 0,

                    record.type_of_card == "Visa" ? 1 : 0
                };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });
                var inputs = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                };
                string predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
                }
                predictions.Add(new FraudPrediction { Order = record, Prediction = predictionResult }); // Adds the animal information and prediction for that animal to AnimalPrediction viewmodel
            }
            return View(predictions);
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    //TwoFactorEnabled = true
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                
                // uncomment for email confirmation (link - https://www.yogihosting.com/aspnet-core-identity-email-confirmation/)
                /*if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index");
                    else
                    {
                        // log email failed 
                    }
                }*/
                
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }
    }
}

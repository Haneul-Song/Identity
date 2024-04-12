namespace Identity.Models
{
    public class FraudPrediction
    {
        public Order Order { get; set; }
        public string Prediction { get; set; }
    }
}

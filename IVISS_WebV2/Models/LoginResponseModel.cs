namespace IVISS_WebV2.Models
{
    public class LoginResponseModel
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string jwtToken { get; set; }
    }
}

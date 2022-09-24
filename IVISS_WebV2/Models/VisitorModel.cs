namespace IVISS_WebV2.Models
{
    public class VisitorModel  
    {
        public object id { get; set; }
        public string lpn { get; set; } = "";
        public string lpna { get; set; } = "";
        public DateTime visitDate { get; set; }
        public string visitorFirstName { get; set; } = "";
        public string visitorMiddleName { get; set; } = "";
        public string visitorLastName { get; set; } = "";
        public string visitorAuthorization { get; set; } = "";
        public string visitorAddress { get; set; } = "";
        public string visitorPhoneNo { get; set; } = "";
        public string visitorMobileNo { get; set; } = "";
        public string visitorEmail { get; set; } = "";
        public string visitorImage { get; set; } = "";
        public string visitorManager { get; set; } = "";
        public string visitorOrganization { get; set; } = "";
        public string visitorFacility { get; set; } = "";
        public string visitorCategory { get; set; } = "";
        public bool checkVisit { get; set; }
    }
}

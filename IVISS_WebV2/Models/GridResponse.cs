using Newtonsoft.Json;

namespace IVISS_WebV2.Models
{

    public class GridResponse
    {
        public string id { get; set; }
        public string licensePlateNumber { get; set; }
        public string licensePlateNumberArabic { get; set; }
        public string driverImage { get; set; }
        public string carMake { get; set; }
        public string carManufacture { get; set; }
        public object gateNumber { get; set; }
        public string licensePlateImage { get; set; }
        public string stitcherImage { get; set; }
        public DateTime timeStamp { get; set; }

        [JsonProperty("default")]
        public bool default1 { get; set; }

        
    }
}

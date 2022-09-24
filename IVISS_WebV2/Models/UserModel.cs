using System.ComponentModel.DataAnnotations;

namespace IVISS_WebV2.Models
{

    public class UserModel
    {

        public string id { get; set; } = "";
        public string firstname { get; set; } = "";

        public string middlename { get; set; } = "";

        public string lastname { get; set; } = "";

        public string username { get; set; } = "";

        public string password { get; set; } = "";

        public string phoneno { get; set; } = "";

        public string roles { get; set; } = "";
        public bool status_flag { get; set; } = false;


    }

}

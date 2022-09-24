using System.ComponentModel.DataAnnotations;

namespace IVISS_WebV2.Models
{

    public class SettingsModel
    {

        public string id { get; set; } = "";
        public string portNo { get; set; }
        public string airWash { get; set; }
        public string barrier { get; set; }
        public string lights { get; set; }
        public string cameras { get; set; }
        public string relay1 { get; set; }
        public string relay2 { get; set; }
        public string relay3 { get; set; }
        public string relay4 { get; set; }

        public string listenPort { get; set; }
        public string relay1Arab { get; set; }
        public string relay2Arab { get; set; }
        public string relay3Arab { get; set; }
        public string relay4Arab { get; set; }
        public string driverCamIP { get; set; }
        public string licenseCamIP { get; set; }
        public string sceneCamIP { get; set; }
        public string driverCamPassword { get; set; }
        public string sceneCamPassword { get; set; }
        public string relay1Port { get; set; }
        public string relay2Port { get; set; }
        public string relay3Port { get; set; }
        public string relay4Port { get; set; }
        public bool ALPREntryLoop { get; set; }
        public bool ALPRExitLoop { get; set; }

        public bool aiPlateFinder { get; set; }
        public bool laserTrigger { get; set; }

        public int retentionDays { get; set; }
        public int comPort { get; set; }
        public string exitDriverCamIP { get; set; }
        public string exitLicenseCamIP { get; set; }
        public int driverRecTimeout { get; set; }
        public string gate_name { get; set; }
        public string ipAddress { get; set; }
       
        public string licenseNo { get; set; }


    }

}

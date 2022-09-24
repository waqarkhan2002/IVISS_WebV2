using IVISS_WebV2.Classes;
using IVISS_WebV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.Digest;
using RestSharp.Serializers.Utf8Json;
using System.Diagnostics;

namespace IVISS_WebV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string BaseUrl = "http://localhost:4000/";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult FillGrid()
        {
            var client = new RestClient(BaseUrl + "licenseplatedata/GetLicense?page=1&itemsperpage=200");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse<List<string>> response = client.Execute<List<string>>(request);


            try
            {
                var result = JsonConvert.DeserializeObject<List<GridResponse>>(response.Content);
              

                var json = JsonConvert.SerializeObject(result);
                return new JsonStringResult(json);
            }
            catch (Exception )
            {
                throw;
            }
        }

        

        public IActionResult LoadALPRStream()
        {

            try
            {
                var client = new RestClient("http://192.168.1.145/stw-cgi/video.cgi?msubmenu=snapshot&action=view")
                {
                    //Digest authentication settings
                    Authenticator = new DigestAuthenticator("admin", "Tasc6314")
                };

                //JSON serializer settings (Utf8Json is used this time)
                client.UseUtf8Json();

                //Request generation (set resource and response data format)
                var request = new RestRequest("", DataFormat.Json);

                //Synchronous call
                var response = client.Get(request);

                var bytes = response.RawBytes;




                string base64ImageRepresentation = Convert.ToBase64String(bytes);
                ReturnedJson json = new ReturnedJson();
                json.Result = base64ImageRepresentation;
                return Json(json);


            }
            catch (Exception)
            {
                ReturnedJson json = new ReturnedJson();
                json.Result = "";// "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";
                return Json(json);
                
            }
        }

        #region Settings

      






        #region Visitor


        public IActionResult FillVisitorFilter(string firstname,string lastname,string lpn)
        {
            var client = new RestClient(BaseUrl + "visitor/AlphaFilter");
            RestRequest request = new RestRequest(Method.POST);

            if (firstname == "" || firstname==null)
                firstname = "null";

            if (lastname == "" || lastname == null)
                lastname = "null";

            if (lpn == "" || lpn == null)
                lpn = "null";

            if(firstname=="null" && lastname=="null" && lpn=="null")
            {
                return FillVisitor();
            }
            request.AddHeader("Content-Type", "application/json");

            var body = @"{
" + "\n" +
 @"   ""data"":{
" + "\n" +
 @"      ""FilterDataList"":[
" + "\n" +
 @"         {
" + "\n" +
 @"            ""FieldName"":""LPN"",
" + "\n" +
 @"            ""Value"":""" + lpn + @"""   
" + "\n" +
 @"         },
" + "\n" +
 @"         {
" + "\n" +
 @"            ""FieldName"":""VisitorLastName"",
" + "\n" +
 @"            ""Value"":""" + lastname + @"""  
" + "\n" +
 @"         },
" + "\n" +
 @"         {
" + "\n" +
 @"            ""FieldName"":""VisitorFirstName"",
" + "\n" +
 @"            ""Value"":""" + firstname + @"""  
" + "\n" +
 @"         }
" + "\n" +
 @"
" + "\n" +
 @"      ]
" + "\n" +
 @"   },
" + "\n" +
 @"   ""params"":{
" + "\n" +
 @"      ""page"":1,
" + "\n" +
 @"      ""itemsperpage"":2000
" + "\n" +
 @"   }
" + "\n" +
 @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
           
            //IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            var response = client.Execute(request);
            try
            {


                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VisitorModel>>(response.Content);
                //if (result != null)
                //{
                //    result = result.Where(x => x.roles == usertype).ToList();

                //}
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return new JsonStringResult(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult FillVisitor()
        {
            var client = new RestClient(BaseUrl + "visitor/GetVisitorFiltered?page=1&itemsperpage=2000");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse<List<string>> response = client.Execute<List<string>>(request);


            try
            {


                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VisitorModel>>(response.Content);
                //if (result != null)
                //{
                //    result = result.Where(x => x.roles == usertype).ToList();

                //}
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return new JsonStringResult(json);
            }
            catch (Exception)
            {
                throw;
            }
        }






        [HttpPost]
        public IActionResult SaveVisitor(string id, string lp, string lparabic, string firstname,string middlename,string organization,string facility,string mobileno,string email,string country,string city, string phone, string manager, string classification,string authorization, string lastname,string address,string region, string authtoken)
        {



            var data = new VisitorModel { id = null, lpn = lp ?? "", lpna = lparabic ?? "", visitDate = DateTime.Now, visitorFirstName = firstname ?? "", visitorMiddleName = middlename ?? "", visitorLastName = lastname ?? "", visitorAuthorization = authorization??"", visitorAddress = address??"",
                visitorPhoneNo = phone??"", visitorMobileNo = mobileno??"",
                visitorEmail = email??"",
                visitorImage = "b64",
                visitorManager = manager??"",
            visitorOrganization=organization??"",
                visitorFacility=facility??"",
                visitorCategory=classification??"",
                checkVisit = true };

            if (id != null)
            {
                data = new VisitorModel
                {
                    id = id,
                    lpn = lp ?? "",
                    lpna = lparabic ?? "",
                    visitDate = DateTime.Now,
                    visitorFirstName = firstname ?? "",
                    visitorMiddleName = middlename ?? "",
                    visitorLastName = lastname ?? "",
                    visitorAuthorization = authorization??"",
                    visitorAddress = address??"",
                    visitorPhoneNo = phone??"",
                    visitorMobileNo = mobileno??"",
                    visitorEmail = email??"",
                    visitorImage = "b64",
                    visitorManager = manager??"",
                    visitorOrganization = organization??"",
                    visitorFacility = facility??"",
                    visitorCategory = classification??"",
                    checkVisit = true
                };

            }
            RestRequest request;
            RestClient client;
            if (id != null)
            {
                client = new RestClient(BaseUrl + "visitor/update");
                request = new RestRequest(Method.PUT);
            }
            else

            {
                client = new RestClient(BaseUrl + "visitor/create");
                request = new RestRequest(Method.POST);
            }



            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + authtoken + "");

            string body = System.Text.Json.JsonSerializer.Serialize(data);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if ((int)response.StatusCode == 200)
                {


                    return Json(new { message = "success" });

                }
                else

                {
                    return Json(new { message = response.Content });
                }

            }
            else
            {
                return Json(new { message = "Unexpected error occured..please try again" });
            }
        }

        [HttpPost]
        public IActionResult DeleteVisitor(string id, string authtoken)
        {





            var data = new { rec_id = id };


            RestRequest request;
            RestClient client;

            client = new RestClient(BaseUrl + "visitor/delete");
            request = new RestRequest(Method.DELETE);




            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + authtoken + "");

            string body = System.Text.Json.JsonSerializer.Serialize(data);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if ((int)response.StatusCode == 200)
                {


                    return Json(new { message = "success" });

                }
                else

                {
                    return Json(new { message = response.Content });
                }

            }
            else
            {
                return Json(new { message = "Unexpected error occured..please try again" });
            }
        }
        #endregion

        #region Settings_Definition
        [HttpPost]
        public IActionResult SaveSettings(string id, string relay1settings, string relay1settingsport, string relay2settings, string relay2settingsport, string relay3settings, string relay3settingsport, string relay4settings, string relay4settingsport, string listenport, string relay1settingsarabic, string relay2settingsarabic, string relay3settingsarabic, string relay4settingsarabic, string ipnumber, bool chkentry, string sceneip, string drivercampassword, bool aiplatefinder, bool lasertrigger, string driverip, string gatename, string  licenseCamIP, string authtoken)
        {



            var data = new SettingsModel
            {
                id = null,
                airWash = "",
                lights = "",
                barrier = "",
                portNo = "",
                cameras = "",
                relay1 = relay1settings ?? "",
                relay1Port = relay1settingsport ?? "",
                relay2 = relay2settings ?? "",
                relay2Port = relay2settingsport ?? "",
                relay3 = relay3settings ?? "",
                relay3Port = relay3settingsport ?? "",
                relay4 = relay4settings ?? "",
                relay4Port = relay4settingsport ?? "",
                listenPort = listenport ?? "",
                relay1Arab = relay1settingsarabic ?? "",
                relay2Arab = relay2settingsarabic ?? "",
                relay3Arab = relay3settingsarabic ?? "",
                relay4Arab = relay4settingsarabic ?? "",
                ipAddress = ipnumber ?? "",
                sceneCamIP = sceneip ?? "",
                ALPREntryLoop = chkentry,
                driverCamPassword = drivercampassword ?? "",
                aiPlateFinder = aiplatefinder,
                laserTrigger = lasertrigger,
                driverCamIP = driverip ?? "",
                gate_name = gatename ?? "",
                comPort = 0,
                licenseCamIP = licenseCamIP,
                licenseNo = "",
                exitDriverCamIP = "",
                exitLicenseCamIP = "",
                retentionDays = 0,
                sceneCamPassword = "",
                driverRecTimeout = 0,
                ALPRExitLoop = false,
                



            };

            if (id != null)
            {
                data = new SettingsModel
                {
                    id = null,
                    airWash = "",
                    lights = "",
                    barrier = "",
                    portNo = "",
                    cameras = "",
                    relay1 = relay1settings ?? "",
                    relay1Port = relay1settingsport ?? "",
                    relay2 = relay2settings ?? "",
                    relay2Port = relay2settingsport ?? "",
                    relay3 = relay3settings ?? "",
                    relay3Port = relay3settingsport ?? "",
                    relay4 = relay4settings ?? "",
                    relay4Port = relay4settingsport ?? "",
                    listenPort = listenport ?? "",
                    relay1Arab = relay1settingsarabic ?? "",
                    relay2Arab = relay2settingsarabic ?? "",
                    relay3Arab = relay3settingsarabic ?? "",
                    relay4Arab = relay4settingsarabic ?? "",
                    ipAddress = ipnumber ?? "",
                    sceneCamIP = sceneip ?? "",
                    ALPREntryLoop = chkentry,
                    driverCamPassword = drivercampassword ?? "",
                    aiPlateFinder = aiplatefinder,
                    laserTrigger = lasertrigger,
                    driverCamIP = driverip ?? "",
                    gate_name = gatename ?? "",
                    comPort = 0,
                    licenseCamIP = licenseCamIP,
                    licenseNo = "",
                    exitDriverCamIP = "",
                    exitLicenseCamIP = "",
                    retentionDays = 0,
                    sceneCamPassword = "",
                    driverRecTimeout = 0,
                    ALPRExitLoop = false
                  
                };

            }
            RestRequest request;
            RestClient client;
          
                client = new RestClient(BaseUrl + "configdata/clientSettings");
                request = new RestRequest(Method.POST);
          



            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + authtoken + "");

            string body = System.Text.Json.JsonSerializer.Serialize(data);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if ((int)response.StatusCode == 200)
                {


                    return Json(new { message = "success" });

                }
                else

                {
                    return Json(new { message = response.Content });
                }

            }
            else
            {
                return Json(new { message = "Unexpected error occured..please try again" });
            }
        }



        public IActionResult FillSettings()
        {
            var client = new RestClient(BaseUrl + "configdata/GetClientSettings");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse<List<string>> response = client.Execute<List<string>>(request);


            try
            {


                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingsModel>(response.Content);
                //if (result != null)
                //{
                //    result = result.Where(x => x.roles == usertype).ToList();

                //}
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return new JsonStringResult(json);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using IVISS_WebV2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using RestSharp;
using IVISS_WebV2.Classes;

namespace IVISS_WebV2.Controllers
{
    public class AccountController : Controller
    {

        string BaseUrl = "http://localhost:4000/";
        public IActionResult Login()
        {
            return View();
        }


       
        [HttpPost]
        public IActionResult LoginSite(string username,string pass)
        {
            //if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            //{
            //    return (RedirectToAction("Error"));
            //}
         


            var client = new RestClient(BaseUrl + "users/authenticate");
            RestRequest request = new RestRequest(Method.POST);


            request.AddHeader("Content-Type", "application/json");


            var data = new AuthenticateModel { username = username, password = pass};

            string body = JsonSerializer.Serialize(data);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if(response.Content!=null)
                {
                    if (response.Content.Contains("message"))
                    {
                        return Json(new { id = -999 });
                    }
                    else
                    {
                        var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(response.Content);
                        return Json(loginResponse);
                    }
                }
               else

                {
                    return Json(new { id = -999 });
                }
               
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }


        public IActionResult FillUsers(string usertype)
        {
            var client = new RestClient(BaseUrl + "users");
            RestRequest request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse<List<string>> response = client.Execute<List<string>>(request);


            try
            {


                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserResponse>>(response.Content);
                if (result != null)
                {
                    result = result.Where(x => x.roles == usertype).ToList();
                    
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return new JsonStringResult(json);
            }
            catch (Exception)
            {
                throw ;
            }
        }

        [HttpPost]
        public IActionResult SaveUser(string id, string FirstName, string LastName, string MiddleName, string Phone, string Username, string Password, string roles, string authtoken)
        {





            var data = new UserModel { id = null, firstname = FirstName ?? "", middlename = MiddleName ?? "", lastname = LastName ?? "", phoneno = Phone ?? "", username = Username, password = Password, roles = roles, status_flag = true };

            if (id != null)
            {
                data = new UserModel { id = id, firstname = FirstName ?? "", middlename = MiddleName ?? "", lastname = LastName ?? "", phoneno = Phone ?? "", username = Username, password = Password, roles = roles, status_flag = true };

            }
            RestRequest request;
            RestClient client;
            if (id != null)
            {
                client = new RestClient(BaseUrl + "users/update");
                request = new RestRequest(Method.PUT);
            }
            else

            {
                client = new RestClient(BaseUrl + "users/create");
                request = new RestRequest(Method.POST);
            }



            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + authtoken + "");

            string body = JsonSerializer.Serialize(data);

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
        public IActionResult DeleteUser(string id,string authtoken)
        {
          


           

            var data = new  { rec_id = id };

            
            RestRequest request;
            RestClient client ;
           
                client = new RestClient(BaseUrl + "users/delete");
                 request = new RestRequest(Method.DELETE);
           
           


            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer "+authtoken+"");

            string body = JsonSerializer.Serialize(data);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if ((int)response.StatusCode ==200)
                {


                    return Json(new { message = "success" });

                }
                else

                {
                    return Json(new { message = response.Content});
                }

            }
            else
            {
                return Json(new { message = "Unexpected error occured..please try again" });
            }
        }
    }
}

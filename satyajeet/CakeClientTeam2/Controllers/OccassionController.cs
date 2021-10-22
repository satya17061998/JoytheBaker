
using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FlowerStore.Controllers
{
    public class OccassionController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        string Baseurl1 = "https://localhost:44322/";

        public IActionResult getCake()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            DateTime TokenExpiry = Convert.ToDateTime(HttpContext.Request.Cookies["Expiry"]);
            DateTime current = DateTime.Now;
            // Console.WriteLine(TokenExpiry + "    expiray time   and current time " + current);
            if (DateTime.Compare(TokenExpiry, current) < 0)  //if token expired redirect to login
            {
                return RedirectToAction("Logout", "Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getCake(string name)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            name = Request.Form["category"];
            
            List<Cake> FlowerInfo = new List<Cake>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl1);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Flow/GetAllFlower");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    FlowerInfo = JsonConvert.DeserializeObject<List<Cake>>(EmpResponse);

                }

                List<Cake> FlowerInfoFilter = new List<Cake>();

                foreach(Cake obj in FlowerInfo)
                {
                    if(obj.Occassion.Equals(name))
                    {
                        FlowerInfoFilter.Add(obj);
                    }
                }

                //returning the employee list to view  
                return View(FlowerInfoFilter);
            }

        }

    }
}

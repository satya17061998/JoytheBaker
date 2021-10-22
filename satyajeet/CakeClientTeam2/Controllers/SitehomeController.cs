using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;
using CakeClientTeam2.Views.Sitehome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CakeClientTeam2.Controllers
{
    public class SitehomeController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        string Baseurl1 = "https://localhost:44322/";
        string Baseurl2 = "https://localhost:44343/";

        public static MyViewModel viewModel;

        public async Task<IActionResult> Home()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            viewModel = new MyViewModel();

            List<Cake> FlowerInfo = new List<Cake>();
            List<Occasion> OccInfo = new List<Occasion>();

            ViewData["Useradmin"] = "False"; //Get Data for user or admin from session
            using var client = new HttpClient();
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

            viewModel.ListA = FlowerInfo;



            using (var client1 = new HttpClient())
            {
                //Passing service base url  
                client1.BaseAddress = new Uri(Baseurl2);

                client1.DefaultRequestHeaders.Clear();
                //Define request data format  
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res1 = await client1.GetAsync("api/Order/GetAllOccasion");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res1.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    OccInfo = JsonConvert.DeserializeObject<List<Occasion>>(EmpResponse);

                }
            }


            viewModel.ListB = OccInfo;

            //returning the employee list to view  
            return View(viewModel);

        }
    }
}

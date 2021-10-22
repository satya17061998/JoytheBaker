﻿
using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlowerStore.Controllers
{
    public class RegisterController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        //string Baseurl1 = "https://localhost:44327/";
        string Baseurl1 = "https://customerapiflower.azurewebsites.net/api/Customer/";

        public IActionResult Register()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Customer cus)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);

                StringContent content = new StringContent(JsonConvert.SerializeObject(cus), Encoding.UTF8, "application/json");

                //using (var response = await httpClient.PostAsync("api/Customer/RegisterCustomer", content))
                using (var response = await httpClient.PostAsync("RegisterCustomer", content))
                {
                    int apiResponse = (int)response.StatusCode;

                    //Console.WriteLine("\n this is what you are looking for \n" + apiResponse + " \nend\n");

                    if (apiResponse == 200)
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }

            ViewBag.errorReg = "**Some error occurred! Try again";

            return View();
        }

    }
}

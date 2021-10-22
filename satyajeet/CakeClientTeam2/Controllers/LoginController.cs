
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlowerStore.Models;
using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;

namespace FlowerStore.Controllers
{
    public class LoginController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        //string Baseurl1 = "https://localhost:44327/";
        string Baseurl1 = "https://customerapiflower.azurewebsites.net/api/Customer/";

        //string Baseurl2 = "https://localhost:44311/";
        string Baseurl2 = "https://jwtteam1.azurewebsites.net";


        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        public IActionResult Login()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Customer cus)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");


            string TokenForLogin;
            string data = JsonConvert.SerializeObject(cus);

            try
            {
                using (var httpClientNew = new HttpClient())
                {
                    httpClientNew.BaseAddress = new Uri(Baseurl2);
                    TokenForLogin = GetToken(httpClientNew, cus);

                    if (!string.IsNullOrEmpty(TokenForLogin))
                    {

                        HttpContext.Response.Cookies.Append("Token", TokenForLogin);
                        //my added code below  

                        IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                        IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                        var tokenExp = decoder.DecodeToObject<JwtTokenExp>(TokenForLogin);
                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tokenExp.exp);
                        DateTime timeExp = dateTimeOffset.LocalDateTime;

                        HttpContext.Response.Cookies.Append("Expiry", timeExp.ToString());
                        //my addedcode above
                        //return View("Index");

                        using (var httpClient = new HttpClient())
                        {
                            //type = "User";
                            httpClient.BaseAddress = new Uri(Baseurl1);

                            //using (var response = await httpClient.GetAsync("api/Customer/CustomerLogin?tempPhone=" + cus.Phone + "&tempPass=" + cus.Password + "&tempType=" + cus.Vendor))
                            using (var response = await httpClient.GetAsync("CustomerLogin?tempPhone=" + cus.Phone + "&tempPass=" + cus.Password + "&tempType=" + cus.Vendor))
                            {
                                int apiResponse = (int)response.StatusCode;

                                //Console.WriteLine("\n this is what you are looking for \n" + apiResponse + " \nend\n");

                                if (apiResponse == 200)
                                {

                                    string apiResponseCus = await response.Content.ReadAsStringAsync();
                                    Customer cust = JsonConvert.DeserializeObject<Customer>(apiResponseCus);

                                    HttpContext.Session.SetString("Username", cust.Name);
                                    HttpContext.Session.SetString("Usertype", cust.Vendor);
                                    HttpContext.Session.SetInt32("Userid", cust.Id);


                                    ViewBag.Username = HttpContext.Session.GetString("Username");
                                    ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

                                    return RedirectToAction("Home", "Sitehome");
                                }
                            }
                        }

                    }
                    ViewBag.error = "**Credentials are wrong";
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            } 
        }

        public IActionResult Logout()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");


            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult GoBack()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            return RedirectToAction("Home", "Sitehome");
        }
        static string GetToken(HttpClient client, Customer user)
        {

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/Auth", data).Result;
            if (response.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;
                return token;
            }

            return null;

        }


    }

}

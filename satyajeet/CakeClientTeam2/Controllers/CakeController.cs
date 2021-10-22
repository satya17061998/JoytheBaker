
using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FlowerClient.Controllers
{
    public class CakeController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        string Baseurl1 = "https://localhost:44322/";
        string Baseurl2 = "https://localhost:44343/";
        string Baseurl3 = "https://localhost:44359/";

        public async Task<ActionResult> GetAllCakes()
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

            List<Cake> FlowerInfo = new List<Cake>();
            ViewData["Useradmin"] = "False"; //Get Data for user or admin from session
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
                //returning the employee list to view  
                return View(FlowerInfo);
            }
        }

        public async Task<ActionResult> AddCake()
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

            List<Occasion> OccInfo = new List<Occasion>();

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

            ViewBag.occList = OccInfo;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddCake(Cake f)
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

            Cake Flobj = new Cake();

            f.Occassion = Request.Form["category"];

            string fileName = Request.Form["floImg"];

            string filePath = "wwwroot/image/flowerimages/" + fileName;
            f.FlImage = System.IO.File.ReadAllBytes(filePath);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                StringContent content = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Flow/RegisterFlower", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Flobj = JsonConvert.DeserializeObject<Cake>(apiResponse);
                }
            }
            return RedirectToAction("Home", "Sitehome");            //file and folder
        }

        [HttpGet]
        public async Task<ActionResult> UpdateCake(int id)
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

            List<Occasion> OccInfo = new List<Occasion>();

            Cake fl = new();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.GetAsync("api/Flow/id?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fl = JsonConvert.DeserializeObject<Cake>(apiResponse);
                }
            }

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

            ViewBag.occList = OccInfo;

            return View(fl);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCake(Cake f)
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

            Cake receivedflower = new();

            f.Occassion = Request.Form["category"];

            string fileName = Request.Form["floImg"];

            string filePath = "wwwroot/image/flowerimages/" + fileName;
            f.FlImage = System.IO.File.ReadAllBytes(filePath);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                int id = f.Id;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("api/Flow/UpdateFlower", content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedflower = JsonConvert.DeserializeObject<Cake>(apiResponse);
                }
            }
            return RedirectToAction("Home", "Sitehome");            //file and folder
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCake(int id)
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

            Cake fl = new();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.GetAsync("api/Flow/id?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fl = JsonConvert.DeserializeObject<Cake>(apiResponse);
                }
            }
            return View(fl);
        }


        [HttpPost]
        // [ActionName("DeleteEmployee")]
        public async Task<ActionResult> DeleteCakeNow(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");


            //int empid = Convert.ToInt32(TempData["empid"]);
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                
                using (var response = await httpClient.DeleteAsync("api/Flow/DeleteFlowerbyId?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Home", "Sitehome");            //file and folder
        }

        [HttpGet]
        public async Task<ActionResult> AddToCart(int id)
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

            var customid = HttpContext.Session.GetInt32("Userid");
            
            Cake f1 = new ();
            Cart c1 = new Cart();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.GetAsync("api/Flow/id?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    f1 = JsonConvert.DeserializeObject<Cake>(apiResponse);
                }


                c1.CustomerId = customid;
                c1.CakeId = id;
                c1.ItemPrice = f1.UnitPrice;
                c1.Quantity = 1;

            }

            ViewBag.fid = id;

            return View(c1);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(Cart c1)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            var id = HttpContext.Session.GetInt32("Userid"); 

            Cake f1 = new();

            c1.CustomerId = id;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl3);
                StringContent content = new StringContent(JsonConvert.SerializeObject(c1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Cart/AdditemtoCart", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("CustomerCart", "Cart");
        }
        
    }
}

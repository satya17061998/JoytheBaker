
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
    public class ManageProfController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        //string Baseurl1 = "https://localhost:44327/";
        string Baseurl1 = "https://customerapiflower.azurewebsites.net/api/Customer/";

        public async Task<IActionResult> ManageProf()
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

            var id = HttpContext.Session.GetInt32("Userid");

            Customer c1 = new Customer();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);

                //using (var response = await httpClient.GetAsync("api/Customer/CustomerbyId?id=" + id))
                using (var response = await httpClient.GetAsync("CustomerbyId?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c1 = JsonConvert.DeserializeObject<Customer>(apiResponse);
                }
            }

            return View(c1);
        }

        [HttpPost]
        public async Task<IActionResult> ManageProf(Customer cus)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Usertype = HttpContext.Session.GetString("Usertype");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(cus), Encoding.UTF8, "application/json");
                //using (var response = await httpClient.PutAsync("api/Customer/UpdateCustomer", content1))
                using (var response = await httpClient.PutAsync("UpdateCustomer", content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                }
            }

            HttpContext.Session.SetString("Username", cus.Name);

            return RedirectToAction("Home", "Sitehome");            //file and folder
        }

    }
}

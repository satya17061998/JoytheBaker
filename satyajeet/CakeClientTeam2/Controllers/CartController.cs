
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
using System.Text;
using System.Threading.Tasks;

namespace FlowerClient.Controllers
{
    public class CartController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        string Baseurl1 = "https://localhost:44359/";        //for cart
        string Baseurl2 = "https://localhost:44322/";           //for flower
        string Baseurl3 = "https://localhost:44343/";          //for order


        [HttpGet]
        public async Task<IActionResult> CustomerCart()
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

            List<Cart> CartInfo = new List<Cart>();

            var customid = HttpContext.Session.GetInt32("Userid");
            
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl1);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Cart/CartbyCustID?id=" + customid);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CartInfo = JsonConvert.DeserializeObject<List<Cart>>(EmpResponse);

                }                
               
            }


            foreach (Cart c in CartInfo)
            {
                Cake f = new ();

                using (var client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri(Baseurl2);

                    using (var response1 = await client1.GetAsync("api/Flow/id?id=" + c.CakeId))
                    {
                        string apiResponse1 = await response1.Content.ReadAsStringAsync();
                        f = JsonConvert.DeserializeObject<Cake>(apiResponse1);
                    }
                }

                c.Cake = f;
            }


            //returning the employee list to view  
            return View(CartInfo);
        }

        [HttpGet]
        public async Task<ActionResult> RemoveFromCart(int id)
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

            Cart fl = new Cart();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.GetAsync("api/Cart/CartByCartID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    fl = JsonConvert.DeserializeObject<Cart>(apiResponse);
                }
            }
            return View(fl);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromCartNow(int CartId)
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

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.DeleteAsync("api/Cart/DeleteItemFromCart?id=" + CartId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("CustomerCart");
        }

        [HttpGet]
        public async Task<ActionResult> PlaceOrder(int id)
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
            
            Cart c1 = new Cart();
            OrderDetail o1 = new OrderDetail();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl1);
                using (var response = await httpClient.GetAsync("api/Cart/CartByCartID?id=" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    c1 = JsonConvert.DeserializeObject<Cart>(apiResponse);
                }

                o1.CartId = c1.CartId;
                o1.CakeId = c1.CakeId;
                o1.CustomerId = c1.CustomerId;
                o1.Totalprice = c1.ItemPrice * c1.Quantity;
                o1.PaymentStatus = "Out for Delievery";

            }
            return View(o1);
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(OrderDetail o1)
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

            int fid = (int)o1.CartId;

            if (o1.Remark.Equals(null))
            {
                o1.Remark = "-NA-";
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl3);
                StringContent content = new StringContent(JsonConvert.SerializeObject(o1), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Order/AddingToOrderDetails", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();                        
                }

            }

            using(var httpClient1 = new HttpClient())
            {
                httpClient1.BaseAddress = new Uri(Baseurl1);
                using (var response1 = await httpClient1.GetAsync("api/Cart/UpdateStatusInCart?id=" + fid))
                {
                    string apiResponse1 = await response1.Content.ReadAsStringAsync();
                }
            }


            return RedirectToAction("YourOrders", "Order");
        }

    }
}

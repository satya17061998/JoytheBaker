using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AuthorizationJWTTeam2.CakeModel;
using CakeClientTeam2.ProjModel;

namespace FlowerStore.Controllers
{
    public class DownloadbillController : Controller
    {
        //string Baseurl = "https://localhost:44318/";
        string Baseurl1 = "https://localhost:44343/";

        [HttpGet]
        public async Task<ActionResult> Index(int id)
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

            OrderDetail ord = new OrderDetail();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl1);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Order/OrderByOrderID?id=" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ord = JsonConvert.DeserializeObject<OrderDetail>(EmpResponse);

                }

            }


            using (MemoryStream stream = new MemoryStream())
            {

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                PdfDocument document = new PdfDocument();

                PdfPage page = document.AddPage();

                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

                DrawWatermark(page, font);

                XGraphics gfx = XGraphics.FromPdfPage(page);

                string ordernum = "Order number: " + ord.OrderId;
                string flowerid = "Flower: " + ord.CakeId;
                string total = "Total price: " + ord.Totalprice;
                string remark = "Remark: " + ord.Remark;
                string status = "Payment status: " + ord.PaymentStatus;

                gfx.DrawString(ordernum, font, XBrushes.Black, 10, 30);
                gfx.DrawString(flowerid, font, XBrushes.Black, 10, 65);
                gfx.DrawString(total, font, XBrushes.Black, 10, 100);
                gfx.DrawString(remark, font, XBrushes.Black, 10, 135);
                gfx.DrawString(status, font, XBrushes.Black, 10, 170);

                document.Save(stream, false);

                return File(stream.ToArray(), "application/pdf", "Invoice.pdf");
            }

        }

        void DrawWatermark(PdfPage page, XFont font)
        {
            string watermark = "FlowerBee";
            // Variation 2: Draw a watermark as an outlined graphical path.
            // NYI: Does not work in Core build.

            // Get an XGraphics object for drawing beneath the existing content.
            var gfx1 = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

            // Get the size (in points) of the text.
            var size = gfx1.MeasureString(watermark, font);

            // Define a rotation transformation at the center of the page.
            gfx1.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx1.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx1.TranslateTransform(-page.Width / 2, -page.Height / 2);

            // Create a graphical path.
            var path = new XGraphicsPath();

            // Create a string format.
            var format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;

            // Add the text to the path.
            // AddString is not implemented in PDFsharp Core.
            path.AddString(watermark, font.FontFamily, XFontStyle.BoldItalic, 150,
            new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
                format);

            // Create a dimmed red pen.
            var pen = new XPen(XColor.FromArgb(128, 255, 0, 0), 2);

            // Stroke the outline of the path.
            gfx1.DrawPath(pen, path);

            gfx1.Dispose();

        }


    }

}

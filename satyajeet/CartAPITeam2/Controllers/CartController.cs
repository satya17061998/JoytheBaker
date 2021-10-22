using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartAPI.Provider;
using CartAPITeam2.CakeModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CartController));
        //public readonly dbFlowerStoreContext db;
        //public CartController(dbFlowerStoreContext db)
        //{
        //    this.db = db;
        //}

   
        public CartController(IProvider _ip)
        {
            ip = _ip;
            // _log4net = log4net.LogManager.GetLogger(typeof(FlowController));
        }

        IProvider ip;
        [HttpGet]

        [Route("CartbyCustID")]
        public ActionResult<Cake> CartbyCustID(int id)
        {
            //List<Cart> Cartlist = new List<Cart>();
            //foreach (var temp in await db.Carts.ToListAsync())
            //{
            //    if (temp.CustomerId == id )//&& temp.Status.Equals("Pending"))
            //    {
            //        Cartlist.Add(temp);
            //    }
            //}
            //return Ok(Cartlist);
            _log4net.Info(" cart detail by " + id + " is invokedt");
            try
            {
                ip = new ProviderClass();
                return Ok(ip.CartbyCustID(id));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("DeleteItemFromCart")]
        public ActionResult<Cart> DeleteItemFromCart(int id)
        {
            //_log4net.Info("Delete item from cart is invoked");
            //Cart temp = await db.Carts.FindAsync(Customerid, FlowerId);
            //db.Carts.Remove(temp);
            //await db.SaveChangesAsync();
            //return Ok();
            _log4net.Info("delete cart  by custmer id & flower id " + id + " is invoked");
            try
            {
                ip = new ProviderClass();
                ip.DeleteItemFromCart(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AdditemtoCart")]
        public async Task<IActionResult> AdditemtoCart(Cart temp)
        {
            //_log4net.Info("Add item to csrt is invoked");
            //Cart temp = new Cart();
            //temp.CustomerId = tempCustomerid;
            //temp.FlowerId = tempFlowerid;
            //temp.Quantity = tempQty;
            //temp.ItemPrice = tempItemPrice;
            //db.Carts.Add(temp);
            //await db.SaveChangesAsync();
            //return Ok();
            _log4net.Info(" Add item to csrt is invoked");
            try
            {
                ip = new ProviderClass();
                ip.AdditemtoCart(temp);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("UpdateStatusInCart")]
        public IActionResult UpdateStatusInCart(int id)
        {
            //Cart temp = await db.Carts.FindAsync(id);
            ////temp.Status = "Placed";
            //db.Entry(temp).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            //return Ok();
            _log4net.Info(" update item to csrt by id "+id+"is invoked");
            try
            {
                ip = new ProviderClass();
                ip.UpdateStatusInCart( id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("CartByCartID")]
        public ActionResult<Cart> CartByCartID(int id)
        {
            _log4net.Info(" cart detail by " + id + " is invokedt");
            try
            {
                ip = new ProviderClass();
                return Ok(ip.CartByCartID(id));
            }
            catch
            {
                return BadRequest();
            }
        }


    }

    


}

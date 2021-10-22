using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.CakeModel;
using CustomerAPI.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        public CustomerController(IProvider _ip)
        {
            ip = _ip;
            // _log4net = log4net.LogManager.GetLogger(typeof(FlowController));
        }

        IProvider ip;
        [HttpPost]
        [Route("RegisterCustomer")]
        public IActionResult RegisterCustomer(Customer temp)
        {
            //_log4net.Info("REgister customer is invoked");
            //db.Customers.Add(temp);
            //await db.SaveChangesAsync();
            //return Ok();

            _log4net.Info("REgister customer is invoked");
            try
            {
                ip = new ProviderClass();
                ip.RegisterCustomer(temp);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("CustomerbyId")]
        public ActionResult<Customer> CustomerbyId(int id)
        { 
        
        //    _log4net.Info("Get Customer by " + id + " is invoked");
        //    Customer temp = await db.Customers.FindAsync(id);
        //    return Ok(temp);

            _log4net.Info("Get Customer by " + id + " is invoked");
            try
            {
                ip = new ProviderClass();
                
                return Ok(ip.CustomerbyId(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer temp)
        {
            //_log4net.Info("Update is invoked");
            //db.Entry(temp).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            //return Ok();

            _log4net.Info("Update is invoked");
            try
            {
                ip = new ProviderClass();
                ip.UpdateCustomer(temp);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("CustomerLogin")]
        public ActionResult<Customer> CustomerLogin(string tempPhone, string tempPass, string tempType)
        {
            //foreach (var temp in await db.Customers.ToListAsync())
            //{
            //    //changed
            //    if (temp.Phone == tempPhone && temp.Password == tempPass && temp.Vendor == tempType)
            //    {
            //        //changed
            //        Customer cus = temp;
            //        return Ok(cus);
            //    }
            //}

            //return NotFound();

            _log4net.Info("Customer login is invoked");
            try
            {
                Customer cus = new Customer();
                ip = new ProviderClass();
                cus=ip.CustomerLogin(tempPhone, tempPass, tempType);
                if( cus != null){
                    return Ok(cus);
                }
                
               // return Ok();
               
            }
            catch
            {
                return NotFound();
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("DeleteCustomerbyId")]
        public ActionResult<Customer> DeleteCustomerbyId(int id)
        {
            //_log4net.Info("Delete customer by " + id + " is invoked");
            //Customer temp = await db.Customers.FindAsync(id);
            //db.Customers.Remove(temp);
            //await db.SaveChangesAsync();
            //return Ok();

            _log4net.Info("Delete customer by " + id + " is invoked");
            try
            {
                ip = new ProviderClass();
                ip.DeleteCustomerbyId(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

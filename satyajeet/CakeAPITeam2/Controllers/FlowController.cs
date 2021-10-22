using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeAPITeam2.CakeModel;
using FlowApi.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(FlowController));

        // List<Flower> flower = new List<Flower>();
        public FlowController(Iprovider _ip)
        {
            ip = _ip;
           // _log4net = log4net.LogManager.GetLogger(typeof(FlowController));
        }

        Iprovider ip;
       // public FlowController(dbFlowerStoreContext db)
        //{
        //    this.db = db;
        //}

        [HttpPost]
        [Route("RegisterCake")]
        public IActionResult RegisterCake(Cake temp)
        {
            //_log4net.Info("Adding new flower is invoked");
            //db.Flowers.Add(temp);
            //await db.SaveChangesAsync();
            //return Ok();
            _log4net.Info("Adding new Cake is invoked");
            try
            {
                ip = new Providerclass();
                ip.RegisterCake(temp);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        //---------------------------------------------Get Flower by ID---------------------------------
        //[HttpGet]
        //[Route("FlowerbyId")]
        //public async Task<ActionResult<Flower>> FlowerbyId(int id)
        //{
        //    _log4net.Info("Get flower by " + id + " is invoked");
        //    Flower temp = await db.Flowers.FindAsync(id);
        //    return Ok(temp);
        //}


        [HttpGet("id")]
        public IActionResult Get(int Id)
        {
            _log4net.Info("Get Cake by " + Id + " is invoked");
            try
            {
                ip = new Providerclass();
                return Ok(ip.GetCakeById(Id));
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("name")]
        public IActionResult Get(string name)
        {
            _log4net.Info("Get Cake by " + name + " is invoked");
            try
            {
                ip = new Providerclass();
                return Ok(ip.GetCakeById(name));
            }
            catch
            {
                return BadRequest();
            }

        }
        //---------------------------------------------Update Cake Details-----------------------------
        [HttpPut]
        [Route("UpdateCake")]
        public  IActionResult UpdateCake(Cake temp)
        {
            //_log4net.Info("UPdate flower is invoked");
            //db.Entry(temp).State = EntityState.Modified;
            //await db.SaveChangesAsync();
            //return Ok();
            _log4net.Info("UPdate flower is invoked");
            try
            {
                ip = new Providerclass();
                ip.UpdateCake(temp);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //---------------------------------------------Delete a Cake by ID----------------------------
        [HttpDelete]
        [Route("DeleteCakebyId")]
        public ActionResult<Cake> DeleteCakebyId(int id)
        {

            _log4net.Info("Delete Cake by " + id + " is invoked");
            try
            {
                ip = new Providerclass();
                ip.DeleteCakebyId(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllCake")]
        public ActionResult<Cake> GetAllCake()
        {
            _log4net.Info(" Http Get Cake Details request");
            try
            {
                ip = new Providerclass();
                return Ok(ip.GetAllCake());
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}

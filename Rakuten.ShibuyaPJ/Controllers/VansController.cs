using Rakuten.ShibuyaPJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rakuten.ShibuyaPJ.Controllers
{
    public class VansController : ApiController
    {
        Van[] vans = new Van[] 
        { 
            new Van { VanNumber="11", VanId=1, NumberOfRedelivered=1 }, 
            new Van { VanNumber="12", VanId=2, NumberOfRedelivered=2 }, 
            new Van { VanNumber="13", VanId=3, NumberOfRedelivered=3 } 
        };

        public IEnumerable<Van> GetAllVans()
        {
            return vans;
        }

        public IHttpActionResult GetVan(int id)
        {
            var van = vans.FirstOrDefault((p) => p.VanId == id);
            if (van == null)
            {
                return NotFound();
            }
            return Ok(van);
        }
    }
}

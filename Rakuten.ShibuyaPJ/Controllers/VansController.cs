using Rakuten.ShibuyaPJ.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Rakuten.ShibuyaPJ.Controllers
{
    public class VansController : ApiController, IVanRepository
    {
        Van[] vans = new Van[] 
        { 
            new Van { VanNumber="11", Latitude="35.660403", Longitude="139.69599" }, 
            new Van { VanNumber="12", Latitude="35.660605", Longitude="139.700372" }
        };

        internal ShibuyaDBEntities context;
        internal DbSet dbSet;

        public VansController()
        {
            this.context = new ShibuyaDBEntities();
            dbSet = context.Set<Van>();
        }

        public VansController(ShibuyaDBEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<Van>();
        }

        //Get api/vans/GetAllVehicles?custLatitude=""&custLongitude="" 
        /// <summary>
        /// App users will get vehicles operating around them on very first screen of customer App.
        /// </summary>
        /// <returns></returns>
        public JsonResult<Van[]> GetAllVehicles(string custLatitude, string custLongitude)
        {
            return Json<Van[]>(vans);
        }

        /// <summary>
        /// Get best possible ETA for this Order.
        /// </summary>
        /// <param name="custLatitude"></param>
        /// <param name="custLongitude"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public JsonResult<object> GetBestETA(string custLatitude, string custLongitude, string productID)
        {
            var vanDetails = new { VanId = string.Empty, ETA = DateTime.MinValue };

            try
            {
                vanDetails = new { VanId = "2", ETA = DateTime.Now };
            }
            catch (HttpResponseException ex)
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                if (ex.Response.StatusCode == HttpStatusCode.BadGateway)
                {
                    responseMessage.StatusCode = HttpStatusCode.BadGateway;
                    responseMessage.ReasonPhrase = "Please provide coordinates.";
                }
                else if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    responseMessage.StatusCode = HttpStatusCode.BadRequest;
                    responseMessage.ReasonPhrase = "Invalid Product Id.";
                }
                else if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = "Something went wrong. Please try again later.";
                }
                throw new HttpResponseException(responseMessage);
            }
            return Json<object>(vanDetails);
        }

  
        //public IHttpActionResult GetVan(int id)
        //{
        //    var van = vans.FirstOrDefault((p) => p.VanId == id);
        //    if (van == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(van);
        //}

        //public static Coordinate GetCoordinates(string region)
        //{
        //    using (var client = new WebClient())
        //    {

        //        string uri = "http://maps.google.com/maps/geo?q='" + region +
        //          "'&output=csv&key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1" +
        //          "-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA";

        //        string[] geocodeInfo = client.DownloadString(uri).Split(',');

        //        return new Coordinate(Convert.ToDouble(geocodeInfo[2]),
        //                   Convert.ToDouble(geocodeInfo[3]));
        //    }
        //}

        static string baseUri =
  "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
        string location = string.Empty;

        public string GetFormatedAddress(string lat, string lng)     
        {
            string requestUri = string.Format(baseUri, lat, lng);

            using (WebClient wc = new WebClient())
            {
                string result = wc.DownloadString(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants()
                              where
                                  elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where
                                   elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    requestUri = res.Value;
                }
            }
            return requestUri;
        }

        public struct Coordinate
        {
            private double lat;
            private double lng;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lng = longitude;
            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lng; } set { lng = value; } }
        }   
    }
}

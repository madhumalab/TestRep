using Rakuten.ShibuyaPJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

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

        //public IEnumerable<Van> GetAllVans()
        //{
        //    return vans;
        //}

        //public IHttpActionResult GetVan(int id)
        //{
        //    var van = vans.FirstOrDefault((p) => p.VanId == id);
        //    if (van == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(van);
        //}

        //public IHttpActionResult GetVehiclePosition(string lng, string ltd)
        //{
        //    GeoLocation
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
            //string lat = "10"; string lng = "17";

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

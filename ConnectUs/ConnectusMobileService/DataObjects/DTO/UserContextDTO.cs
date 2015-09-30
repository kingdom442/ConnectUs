using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO
{
    public class UserContextDTO
    {
        public string UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO
{
    public class FindUsersRequestData
    {
        public string UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double MaxDistance { get; set; }
        public bool? AlreadyCompared { get; set; } //Find users that habe already been compared or not
    }
}
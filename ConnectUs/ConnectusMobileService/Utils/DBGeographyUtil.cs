using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace ConnectusMobileService.Utils
{
    public class DBGeographyUtil
    {
        internal static DbGeography getDBGeography(double longitude, double latitude)
        {
            var locationText = string.Format(System.Globalization.CultureInfo.InvariantCulture, "POINT({0} {1})", longitude, latitude);
            return DbGeography.FromText(locationText);
        }
    }
}
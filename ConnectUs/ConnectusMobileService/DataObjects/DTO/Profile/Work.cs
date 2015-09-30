using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO.Profile
{
    public struct Work
    {
        public string name, description; //CompanyName...
        public string type, position;
        public string city;
        public string dateFrom, dateTo;

        public enum WorkType
        {
            COMPANY, VOLUNTARY
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO.Profile
{
    public class Work: ProfileDataItem
    {
        public string name, description; //CompanyName...
        public string type, position;
        public string city;
        public string dateFrom, dateTo;

        public enum WorkType
        {
            COMPANY, VOLUNTARY
        }

        protected override string CompareFieldValue()
        {
            return name;
        }
    }
}
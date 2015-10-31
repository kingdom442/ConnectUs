using ConnectusMobileService.DataObjects.DTO.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Profile
{
    public class Education: ProfileDataItem
    {
        public string name;
        public string type;
        public int? yearFrom, yearTo;

        protected override string CompareFieldValue()
        {
            return name;
        }
    }
}
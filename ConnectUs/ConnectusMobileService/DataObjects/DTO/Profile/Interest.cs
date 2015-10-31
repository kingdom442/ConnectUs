using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO.Profile
{
    public class Interest: ProfileDataItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public InterestType type { get; set; }

        protected override string CompareFieldValue()
        {
            return name + type;
        }
    }

    public enum InterestType
    {
        SPORT, MUSIC, TEAM, ORGANISATION, ATHLETE
    }
}
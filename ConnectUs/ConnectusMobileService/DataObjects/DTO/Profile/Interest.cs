using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO.Profile
{
    public class Interest: IEquatable<Interest>
    {
        public string name { get; set; }
        public string description { get; set; }
        public InterestType type { get; set; }


        public bool Equals(Interest obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            if (!name.Equals(((Interest)obj).name))
                return false;
            if (description != ((Interest)obj).description)
                return false;
            if (type != ((Interest)obj).type)
                return false;

            return true;
        }



        public override int GetHashCode()
        {
            int hash = 17;
            if(name != null)
                hash = hash * 31 + name.GetHashCode();
            if(description != null)
                hash = hash * 31 + description.GetHashCode();
            hash = hash * 31 + type.GetHashCode();
            return hash;
        }
    }

    public enum InterestType
    {
        SPORT, MUSIC, TEAM, ORGANISATION
    }
}
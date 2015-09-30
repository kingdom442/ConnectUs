using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Profile
{
    public struct Education
    {
        public string name;
        public string type;
        public int? yearFrom, yearTo;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            return name.Equals(((Education)obj).name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
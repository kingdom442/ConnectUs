using ConnectusMobileService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO.Profile
{
    public abstract class ProfileDataItem
    {
        //Can be changed in the subclasses constructor
        protected readonly double jaroWinklerDistanceTreshold = 0.1;

        protected abstract string CompareFieldValue();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ProfileDataItem))
                return false;
            else if (JaroWinklerDistance.distance(((ProfileDataItem)obj).CompareFieldValue(), CompareFieldValue()) > jaroWinklerDistanceTreshold)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return this.CompareFieldValue().GetHashCode();
        }
    }
}
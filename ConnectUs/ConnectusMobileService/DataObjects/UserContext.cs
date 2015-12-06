using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace ConnectusMobileService.DataObjects
{
    public class UserContext: EntityData
    {
        public DbGeography Location { get; set; }

        public string AccountRefId { get; set; }
        
        [ForeignKey("AccountRefId")]
        public virtual Account Account { get; set; }

        public override bool Equals(object obj)
        {
            if(!(obj is UserContext))
                return false;
            if (Id == ((UserContext)obj).Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
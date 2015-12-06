using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{
    public class UserContact: EntityData
    {
        [Index(IsUnique = true), ForeignKey("User")]
        public string UserId { get; set; }

        public virtual Account User { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        public string PhoneNr { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }
    }
}
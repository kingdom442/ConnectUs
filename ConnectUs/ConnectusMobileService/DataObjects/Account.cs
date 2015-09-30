using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConnectusMobileService.DataObjects
{
    public class Account : EntityData
    {
        [Required]
        [MaxLength(30)]
        [Index("UX_Username", IsUnique = true)]
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string MailAddress { get; set; }
        
        [MaxLength(30)]
        public string FacebookId { get; set; }

        public virtual UserContact UserContact { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
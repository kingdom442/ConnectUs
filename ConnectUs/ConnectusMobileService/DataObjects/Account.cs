using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ConnectusMobileService.DataObjects
{
    public class Account : EntityData
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string MailAddress { get; set; }
        
        [MaxLength(30)]
        public string FacebookId { get; set; }

        [MaxLength(30)]
        public string LinkedInId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Event> Events { get; set; }

        public bool Equals(Account x, Account y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(string obj)
        {
            return 0;
        }
    }
}
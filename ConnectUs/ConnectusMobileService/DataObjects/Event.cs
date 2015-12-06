using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{
    public class Event: EntityData
    {
        public Event()
        {
            this.Accounts = new HashSet<Account>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(30)]
        public string Place { get; set; }
        public DbGeography Location { get; set; }

        [Required]
        public DateTimeOffset FromDate { get; set; }
        [Required]
        public DateTimeOffset ToDate { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } //Participating users
    }
}
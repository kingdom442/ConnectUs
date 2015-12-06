using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{
    public class ConnectRequest: EntityData
    {
        [ForeignKey("RequestUser")]
        [Index("UX_ConnectRequest", 1, IsUnique = true)]
        public string RequestUserId { get; set; }

        [ForeignKey("ConnectUser")]
        [Index("UX_ConnectRequest", 2, IsUnique = true)]
        public string ConnectUserId { get; set; }
        
        public virtual Account RequestUser { get; set; }
        public virtual Account ConnectUser { get; set; }
        
        public bool? Accepted { get; set; }
    }
}
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{
    public class UserInfoDetail: EntityData
    {
        public UserInfoDetail() { this.JsonInfo = "";  this.NetworkId = (Int16)NetworkType.CONNECT_US; }
        [MaxLength(10000)]
        public string JsonInfo { get; set; }

        [NotMapped]
        public virtual UserInfo UserInfo { get; set; }


        [ForeignKey("Network")]
        [DefaultValue(NetworkType.CONNECT_US)]
        public Int16 NetworkId { get; set; }

        public virtual Network Network { get; set; }

        public void SetNetork(Network network)
        {
            this.Network = network;
            this.NetworkId = network.Id;
        }
    }
}
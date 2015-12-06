using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{

    //TODO: Implement correct order by (ConnectUs - Facebook - Linked in...)
    public class Network
    {
        public Network() { }
        public Network(NetworkType networkType)
        {
            this.Id = (Int16)networkType;
            this.Name = networkType.ToString();
        }

        [Key]
        [Required]
        public Int16 Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserComparison> UserComparisons { get; set; }


        public virtual NetworkType NetworkType { get { return (NetworkType)Id; } }

        internal static IEnumerable<Network> GetAllNetworks()
        {
            foreach (NetworkType networkType in Enum.GetValues(typeof(NetworkType)).Cast<NetworkType>().ToList())
            {
                yield return new Network(networkType);
            }
        }
    }

    public enum NetworkType : Int16
    {
        FACEBOOK = 1, LINKED_IN = 2, TWITTER = 3, CONNECT_US = 4

    }
}
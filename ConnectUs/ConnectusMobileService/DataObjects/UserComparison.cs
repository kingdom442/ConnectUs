using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.DataObjects.Profile;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects
{
    /// <summary>
    /// The result of the comparison is saved in the form of 3 different JsonStrings (same structure as the Json in UserInfoDetail)
    /// One contains the equal parts, one the parts that are only available in the profile of user1 and the third part represents the things that are only present for user2.
    /// </summary>
    public class UserComparison : EntityData
    {
        public UserComparison() { EqualProfileData = new ProfileData(); OnlyUserProfileData = new ProfileData(); OnlyCompUserProfileData = new ProfileData(); }
        public UserComparison(string userId, string compUserId)
        {
            this.UserId = userId;
            this.CompUserId = compUserId;
        }

        [Required]
        [ForeignKey("User")]
        [Index("UX_UseComparison", 1, IsUnique = true)]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("CompUser")]
        [Index("UX_UseComparison", 2, IsUnique = true)]
        public string CompUserId { get; set; }

        public virtual Account User { get; set; }
        public virtual Account CompUser { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index("UX_UseComparison", 3, IsUnique = true, IsClustered =true)]
        [TableColumn(TableColumnType.CreatedAt)]
        public new DateTimeOffset? CreatedAt { get; set; }

        public virtual IList<Network> Networks { get; set; }

        private string _equalJson;
        [MaxLength(10000)]
        public string EqualJson {
            get
            {
                return JsonConvert.SerializeObject(this.EqualProfileData, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            set
            {
                this.EqualProfileData = JsonConvert.DeserializeObject<ProfileData>(value);
                _equalJson = value;
            }
        }

        private string _onlyUserJson;
        [MaxLength(10000)]
        public string OnlyUserJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.OnlyUserProfileData, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            set
            {
                this.OnlyUserProfileData = JsonConvert.DeserializeObject<ProfileData>(value);
                _onlyUserJson = value;
            }
        }

        private string _onlyCompUserJson;
        [MaxLength(10000)]
        public string OnlyCompUserJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.OnlyCompUserProfileData, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            set
            {
                this.OnlyCompUserProfileData = JsonConvert.DeserializeObject<ProfileData>(value);
                _onlyCompUserJson = value;
            }
        }


        [JsonIgnore]
        [NotMapped]
        public ProfileData EqualProfileData { get; set; }
     
        [JsonIgnore]
        [NotMapped]
        public ProfileData OnlyUserProfileData { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ProfileData OnlyCompUserProfileData { get; set; }
    }
}
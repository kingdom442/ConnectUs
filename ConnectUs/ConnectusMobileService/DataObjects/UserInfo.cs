using ConnectusMobileService.DataObjects.Profile;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectusMobileService.DataObjects
{
    /// <summary>
    /// Optional information about the user that doesn't come from other networks (job, age...)
    /// </summary>
    public class UserInfo : EntityData
    {
        public void SetProfileInfo(ProfileData profileData)
        {
            this.Gender = profileData.Gender;
            this.FirstName = profileData.FirstName;
            this.LastName = profileData.LastName;
            this.Age = profileData.Age;
            if(profileData.Bio != null)
                this.Bio = profileData.Bio;
            if (profileData.About != null)
                this.About = profileData.About;
            if (profileData.ProfilePicUrl != null)
                this.ProfilePicUrl = profileData.ProfilePicUrl;
        }

        public UserInfo() { this.UserInfoDetails = new HashSet<UserInfoDetail>(); }

        [Index(IsUnique =true)]
        [ForeignKey("Account")]
        public string UserId { get; set; }

        [MaxLength(2000)]
        public string ProfilePicUrl { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string About { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Account Account { get; set; }
       
        public virtual ICollection<UserInfoDetail> UserInfoDetails { get; set; } //user details from profile providers
    }
}
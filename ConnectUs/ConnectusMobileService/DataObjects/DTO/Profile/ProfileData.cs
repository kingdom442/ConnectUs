using ConnectusMobileService.DataObjects.DTO.Profile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Profile
{
    public class ProfileData
    {
        public ProfileData()
        {
            this.EducationList = new List<Education>();
            this.WorkHistory = new List<Work>();
            this.Interests = new List<Interest>();
        }

        public string ProfilePicUrl { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public string Bio { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
   
        public string Industry { get; set; }

        //The URL to the profile
        public string ProfileLink { get; set; }
        public string EmailAddress { get; set; }

        public List<Education> EducationList { get; set; }
        public List<Work> WorkHistory { get; set; }
        public List<Interest> Interests { get; set; }

        public string AsJsonString()
        {
            //Serialize Object
            return JsonConvert.SerializeObject(this);
        }


        internal static IEnumerable<ProfileDataObjects> GetAllProfileDataObjects()
        {
            return Enum.GetValues(typeof(ProfileDataObjects)).Cast<ProfileDataObjects>();
        }
    }
}
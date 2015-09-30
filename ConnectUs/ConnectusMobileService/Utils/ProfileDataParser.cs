using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ConnectusMobileService.DataObjects.Profile;
using ConnectusMobileService.DataObjects.DTO.Profile;

namespace ConnectusMobileService.Utils
{
    /// <summary>
    /// Converts the data of the different profileproviders to a universal "ConnectUs-Structure"
    /// </summary>
    public class ProfileDataParser
    {
        internal static ProfileData ParseFBData(JObject fbJson)
        {
            ProfileData profileData = new ProfileData() { ProfilePicUrl = (string)fbJson[FBFields.PICTURE.ToString().ToLower()]["data"]["url"], FirstName = (string)fbJson[FBFields.FIRST_NAME.ToString().ToLower()], LastName = (string)fbJson[FBFields.LAST_NAME.ToString().ToLower()], Description = (string)fbJson[FBFields.BIO.ToString().ToLower()], Gender = (string)fbJson[FBFields.GENDER.ToString().ToLower()] };

            JArray jEducations = (JArray)fbJson[FBFields.EDUCATION.ToString().ToLower()];
            foreach(JObject jEducation in jEducations)
            {
                profileData.EducationList.Add(new Education() { name = (string)jEducation["school"]["name"], type = (string)jEducation["type"], yearTo = ((jEducation["year"] != null) ? (int?)jEducation["year"]["name"]: null) });
            }

            JArray jWorkHistory = (JArray)fbJson[FBFields.WORK.ToString().ToLower()];
            foreach (JObject jWork in jWorkHistory)
            {
                profileData.WorkHistory.Add(new Work() { name = (string)jWork["employer"]["name"], type = "Company",
                    dateFrom = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("start_date"))) ? (string)jWork["start_date"] : null,
                    dateTo = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("end_date"))) ? (string)jWork["end_date"]: null,
                    city = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("location.name"))) ? (string)jWork["location"]["name"] : null,
                    description = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("/description"))) ? (string)jWork["description"]: null,
                    position = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("position"))) ? (string)jWork["position"]: null
                });
            }


            return profileData;
        }
    }
}
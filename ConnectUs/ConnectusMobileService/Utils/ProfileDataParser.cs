using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ConnectusMobileService.DataObjects.Profile;
using ConnectusMobileService.DataObjects.DTO.Profile;
using System.Reflection;
using System.ComponentModel;

namespace ConnectusMobileService.Utils
{
    /// <summary>
    /// Converts the data of the different profileproviders to a universal "ConnectUs-Structure"
    /// </summary>
    public class ProfileDataParser
    {
        internal static ProfileData ParseFBData(JObject fbJson)
        {
            ProfileData profileData = new ProfileData() { ProfilePicUrl = (string)fbJson[FBFields.PICTURE.ToString().ToLower()]["data"]["url"], FirstName = (string)fbJson[FBFields.FIRST_NAME.ToString().ToLower()], LastName = (string)fbJson[FBFields.LAST_NAME.ToString().ToLower()], Bio = (string)fbJson[FBFields.BIO.ToString().ToLower()], About = (string)fbJson[FBFields.ABOUT.ToString().ToLower()],  Gender = (string)fbJson[FBFields.GENDER.ToString().ToLower()], ProfileLink = (string)fbJson[FBFields.LINK.ToString().ToLower()] };

            try {
                JArray jEducations = (JArray)fbJson[FBFields.EDUCATION.ToString().ToLower()];
                foreach (JObject jEducation in jEducations)
                {
                    profileData.EducationList.Add(new Education() { name = (string)jEducation["school"]["name"], type = (string)jEducation["type"], yearTo = ((jEducation["year"] != null) ? (int?)jEducation["year"]["name"] : null) });
                }
            } catch(Exception e)
            {
                
            }
            try
            {
                JArray jWorkHistory = (JArray)fbJson[FBFields.WORK.ToString().ToLower()];
                foreach (JObject jWork in jWorkHistory)
                {
                    profileData.WorkHistory.Add(new Work()
                    {
                        name = (string)jWork["employer"]["name"],
                        type = "Company",
                        dateFrom = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("start_date"))) ? (string)jWork["start_date"] : null,
                        dateTo = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("end_date"))) ? (string)jWork["end_date"] : null,
                        city = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("location.name"))) ? (string)jWork["location"]["name"] : null,
                        description = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("/description"))) ? (string)jWork["description"] : null,
                        position = (!JsonUtil.IsNullOrEmpty(jWork.SelectToken("position"))) ? (string)jWork["position"] : null
                    });
                }
            }
            catch (Exception e)
            {

            }
            try
            {
                JArray jTeams = (JArray)fbJson[FBFields.FAVORITE_TEAMS.ToString().ToLower()];
                foreach (JObject jTeam in jTeams)
                {
                    profileData.Interests.Add(new Interest()
                    {
                        name = (!JsonUtil.IsNullOrEmpty(jTeam.SelectToken("name"))) ? (string)jTeam["name"] : null,
                        type = InterestType.TEAM
                    });
                }
            }
            catch (Exception e)
            {

            }

            try {
                JArray jAthletes = (JArray)fbJson[FBFields.FAVORITE_ATHLETES.ToString().ToLower()];
                foreach (JObject jAthlete in jAthletes)
                {
                    profileData.Interests.Add(new Interest()
                    {
                        name = (!JsonUtil.IsNullOrEmpty(jAthlete.SelectToken("name"))) ? (string)jAthlete["name"] : null,
                        type = InterestType.ATHLETE
                    });
                }
            } catch(Exception e)
            {

            }

            return profileData;
        }


        internal static ProfileData ParseLinkedInData(JObject jsonProfileData)
        {
            ProfileData profileData = new ProfileData() { ProfilePicUrl = (string)jsonProfileData[GetEnumDescription(LinkedInFields.PICTUREURL)],
                FirstName = (string)jsonProfileData[GetEnumDescription(LinkedInFields.FIRSTNAME)], LastName = (string)jsonProfileData[GetEnumDescription(LinkedInFields.LASTNAME)],
                Bio = (string)jsonProfileData[GetEnumDescription(LinkedInFields.SUMMARY)], About = (string)jsonProfileData[GetEnumDescription(LinkedInFields.HEADLINE)],
                ProfileLink = (string)jsonProfileData[GetEnumDescription(LinkedInFields.PUBLICPROFILEURL)], EmailAddress = (string)jsonProfileData[GetEnumDescription(LinkedInFields.EMAILADDRESS)]
            , Industry = (string)jsonProfileData[GetEnumDescription(LinkedInFields.INDUSTRY)] };
            

            return profileData;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
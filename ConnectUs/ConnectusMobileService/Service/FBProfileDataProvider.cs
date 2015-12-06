using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectusMobileService.DataObjects;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using ConnectusMobileService.Utils;
using ConnectusMobileService.DataObjects.Profile;

namespace ConnectusMobileService.Service
{
    public class FBProfileDataProvider: ETLProfileDataProvider
    {
        FacebookClient client;

        public FBProfileDataProvider(string accessToken)
        {
            client = new FacebookClient(accessToken);
        }

        protected override async Task<object> Extract()
        {
            List<FBFields> allFields = Enum.GetValues(typeof(FBFields)).Cast<FBFields>().ToList();
            string fieldStr = "";
            allFields.ForEach(x => fieldStr += (x == FBFields.PICTURE) ? "picture.width(200).height(200)" : x.ToString().ToLower() + ", ");
            fieldStr = fieldStr.Remove(fieldStr.Length - 1);
            try
            {
                dynamic me = await client.GetTaskAsync("me?fields=" + fieldStr);
                return me;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return null;
        }

        protected override ProfileData Transform(object extractedData)
        {
            JObject jsonFBProfileData = JObject.Parse(JsonConvert.SerializeObject(extractedData));

            ProfileData fbProfileData = ProfileDataParser.ParseFBData(jsonFBProfileData);

            return fbProfileData;
        }

        protected override void Load(ProfileData profileData)
        {
            //No loading action for this implementation
        }
    }
}
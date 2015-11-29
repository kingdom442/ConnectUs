
using ConnectusMobileService.DataObjects;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConnectusMobileService.Utils;
using ConnectusMobileService.DataObjects.Profile;
using LinkedIn.Api.Client.Owin;
using LinkedIn.Api.Client.Owin.Profiles;
using System.Web;

namespace ConnectusMobileService.Service
{
    public class LinkedInProfileDataProvider : ETLProfileDataProvider
    {
        LinkedInApiClient client;

        public LinkedInProfileDataProvider(string accessToken)
        {
            client = new LinkedInApiClient(HttpContext.Current.GetOwinContext().Request, accessToken);
        }

        protected override async Task<object> Extract()
        {
            var profileApi = new LinkedInProfileApi(client);
            var userProfile = await profileApi.GetFullProfileAsync();

            return userProfile;
        }

        protected override ProfileData Transform(object extractedData)
        {
            JObject jsonProfileData = JObject.Parse(JsonConvert.SerializeObject(extractedData));

            ProfileData linkedInProfileData = ProfileDataParser.ParseLinkedInData(jsonProfileData);

            return linkedInProfileData;
        }

        protected override void Load(ProfileData profileData)
        {
            //No loading action for this implementation
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ConnectusMobileService.DataObjects.Profile;

namespace ConnectusMobileService.Service
{
    public abstract class ETLProfileDataProvider: IProfileDataProvider
    {
        public virtual async Task<ProfileData> GetUserInfo()
        {
            object extractedData = await Extract();
            ProfileData transformedData = Transform(extractedData);
            Load(transformedData);
            return transformedData;
        }

        abstract protected Task<object> Extract();
        abstract protected ProfileData Transform(object extractedData);
        abstract protected void Load(ProfileData profileData);
    }
}
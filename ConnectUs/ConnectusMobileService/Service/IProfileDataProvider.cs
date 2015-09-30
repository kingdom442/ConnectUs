using ConnectusMobileService.DataObjects;
using ConnectusMobileService.DataObjects.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectusMobileService.Service
{
    interface IProfileDataProvider
    {
        Task<ProfileData> GetUserInfo();
    }
}

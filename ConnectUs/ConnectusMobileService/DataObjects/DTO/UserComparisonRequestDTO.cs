using ConnectusMobileService.DataObjects.DTO.Profile;
using ConnectusMobileService.DataObjects.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO
{
    /// <summary>
    /// This class contains informations about a requested usercomparison
    /// </summary>
    public class UserComparisonRequestDTO
    {
        public string UserId { get; set; }
        public string CompUserId { get; set; }

        /// <summary>
        /// ProfileInformation from these networks should be included
        /// If there are no elements -> all networks will be considered
        /// </summary>
        public IEnumerable<NetworkType> Networks { get; set; }
        /// <summary>
        /// ProfileDataObjects listed here should be included
        /// If there are no elements -> all will be considered
        /// </summary>
        public IEnumerable<ProfileDataObjects> CompareObjects { get; set; }
    }
}
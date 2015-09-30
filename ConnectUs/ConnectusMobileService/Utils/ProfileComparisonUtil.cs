using ConnectusMobileService.DataObjects;
using ConnectusMobileService.DataObjects.DTO.Profile;
using ConnectusMobileService.DataObjects.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Utils
{
    public class ProfileComparisonUtil
    {
        /// <summary>
        /// Compares 2 userprofile.
        /// </summary>
        /// <param name="userProfile"></param>
        /// <param name="compUserProfile"></param>
        /// <param name="objectsToCompare"> optional (if not given, all objects will be taken into account)</param>
        /// <param name="networks"> optional (if not given, all networks will be added to the resulting usercomparison)</param>
        /// <returns></returns>
        internal static UserComparison CompareUserProfiles(ProfileData userProfile, ProfileData compUserProfile, IEnumerable<ProfileDataObjects> objectsToCompare = null, IEnumerable<Network> networks = null)
        {
            UserComparison userComparison = new UserComparison();
            if(objectsToCompare == null)
                objectsToCompare = ProfileData.GetAllProfileDataObjects();
            if (networks == null)
                networks = Network.GetAllNetworks();

            userComparison.Networks = networks.ToList();

            if (objectsToCompare.Contains(ProfileDataObjects.Education))
                CompareEducation(ref userComparison, userProfile, compUserProfile);
            if(objectsToCompare.Contains(ProfileDataObjects.Work))
                CompareWork(ref userComparison, userProfile, compUserProfile);

            return userComparison;
        }

        private static void CompareEducation(ref UserComparison userComparison, ProfileData userProfile, ProfileData compUserProfile)
        {
            IEnumerable<Education> onlyProfile1Educations = userProfile.EducationList.Except(compUserProfile.EducationList);
            IEnumerable<Education> onlyProfile2Educations = compUserProfile.EducationList.Except(userProfile.EducationList);

            IEnumerable<Education> bothProfileEducations = userProfile.EducationList.Except(onlyProfile1Educations);

            userComparison.EqualProfileData.EducationList = bothProfileEducations.ToList();
            userComparison.OnlyUserProfileData.EducationList = onlyProfile1Educations.ToList();
            userComparison.OnlyCompUserProfileData.EducationList = onlyProfile2Educations.ToList();
        }


        private static void CompareWork(ref UserComparison userComparison, ProfileData userProfile, ProfileData compUserProfile)
        {
            IEnumerable<Work> onlyProfile1WorkHistory = userProfile.WorkHistory.Except(compUserProfile.WorkHistory);
            IEnumerable<Work> onlyProfile2WorkHistory = compUserProfile.WorkHistory.Except(userProfile.WorkHistory);

            IEnumerable<Work> bothProfileWorkHistory = userProfile.WorkHistory.Except(onlyProfile1WorkHistory);

            userComparison.EqualProfileData.WorkHistory = bothProfileWorkHistory.ToList();
            userComparison.OnlyUserProfileData.WorkHistory = onlyProfile1WorkHistory.ToList();
            userComparison.OnlyCompUserProfileData.WorkHistory = onlyProfile2WorkHistory.ToList();
        }
    }
}
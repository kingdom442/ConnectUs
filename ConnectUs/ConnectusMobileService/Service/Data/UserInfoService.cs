using ConnectusMobileService.DataObjects;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Service.Data
{
    public class UserInfoService
    {
        public static BasicUserInfoDTO GetBasicUserInfo(string userId)
        {
            MobileServiceContext context = new MobileServiceContext();
            BasicUserInfoDTO userInfo = new BasicUserInfoDTO();
            userInfo.AccountId = userId;
            UserInfo ui = context.UserInfos.Include("Account").Include("UserInfoDetails").First(uinfo => uinfo.UserId == userId);
            userInfo.Age = (userInfo.Age == null) ? ui.Age : userInfo.Age;
            userInfo.Status = ui.Bio;
            userInfo.About = ui.About;
            userInfo.FacebookConnected = (userInfo.FacebookConnected || ui.UserInfoDetails.Any(x => x.NetworkId == (Int16)NetworkType.FACEBOOK)) ? true : false;
            userInfo.Gender = (userInfo.Gender == null) ? ui.Gender : userInfo.Gender;
            userInfo.Username = (userInfo.Username == null) ? ui.Account.Username : userInfo.Username;
            userInfo.FirstName = (userInfo.FirstName == null) ? ui.FirstName : userInfo.FirstName;
            userInfo.LastName = (userInfo.LastName == null) ? ui.LastName : userInfo.LastName;
            userInfo.ProfilePicUrl = (userInfo.ProfilePicUrl == null) ? ui.ProfilePicUrl : userInfo.ProfilePicUrl;
            return userInfo;
        }
    }
}
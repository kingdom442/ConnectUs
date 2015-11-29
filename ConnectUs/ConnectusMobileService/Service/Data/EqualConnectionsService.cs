using ConnectusMobileService.DataObjects;
using ConnectusMobileService.DataObjects.DTO;
using ConnectusMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Service.Data
{
    public class EqualConnectionsService
    {
        public static List<BasicUserInfoDTO> GetEqualConnections(string userId, string user2Id)
        {
            MobileServiceContext context = new MobileServiceContext();
            List<BasicUserInfoDTO> equal = new List<BasicUserInfoDTO>();

            List<Account> userConnections = context.ConnectRequests.Where(cr => cr.RequestUserId == userId || cr.ConnectUserId == userId).Select(cr => ((cr.ConnectUserId == userId) ? cr.RequestUser : cr.ConnectUser)).ToList();
            List<Account> user2Connections = context.ConnectRequests.Where(cr => cr.RequestUserId == user2Id || cr.ConnectUserId == user2Id).Select(cr => ((cr.ConnectUserId == user2Id) ? cr.RequestUser : cr.ConnectUser)).ToList();

            IEnumerable<Account> intersect = userConnections.Intersect<Account>(user2Connections);

            foreach(Account account in intersect)
            {
                BasicUserInfoDTO userInfo = new BasicUserInfoDTO();
                userInfo.AccountId = account.Id;
                UserInfo ui = context.UserInfos.First(uinfo => uinfo.UserId == account.Id);
                userInfo.Age = ui.Age;
                userInfo.Status = ui.About;
                userInfo.Username = account.Username;
                userInfo.FirstName = ui.FirstName;
                userInfo.LastName = ui.LastName;
                userInfo.ProfilePicUrl = ui.ProfilePicUrl;
                equal.Add(userInfo);
            }
            return equal;
        }
    }
}
using ConnectusMobileService.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.Utils
{
    public static class JsonUtil
    {
        public static bool IsNullOrEmpty(this JToken token)
        {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }

        internal static UserInfo MergeUserInfoDetails(IEnumerable<UserInfo> userInfos)
        {
            if (!userInfos.Any())
            {
                //No Data available for user
                return null;
            }
            UserInfo merged = userInfos.First();
            if (merged.UserInfoDetail == null)
                merged.UserInfoDetail = new UserInfoDetail() { UserInfoId = merged.Id, JsonInfo = "{}" };
            JObject mergedJson = (merged.UserInfoDetail.JsonInfo != null) ? JObject.Parse(merged.UserInfoDetail.JsonInfo): new JObject();

            foreach (UserInfo uiDet in userInfos.Where(x => x.Id != merged.Id))
            {
                JObject toMergeJson = (uiDet.UserInfoDetail != null && uiDet.UserInfoDetail.JsonInfo != null) ? JObject.Parse(uiDet.UserInfoDetail.JsonInfo): null;
                if (mergedJson == null)
                    mergedJson = toMergeJson;
                else
                {
                    mergedJson.Merge(toMergeJson, new JsonMergeSettings()
                    {
                        // union array values together to avoid duplicates
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                }
            }
            if (mergedJson != null)
            {
                merged.UserInfoDetail.JsonInfo = mergedJson.ToString();
            }
            return merged;
        }

    }
}
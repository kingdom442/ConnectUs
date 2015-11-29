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

        internal static UserInfoDetail MergeUserInfoDetails(IEnumerable<UserInfoDetail> userInfos)
        {
            if (!userInfos.Any())
            {
                return new UserInfoDetail();
            }
            UserInfoDetail merged = userInfos.First();
            if (merged == null)
                merged = new UserInfoDetail();
            JObject mergedJson = (merged.JsonInfo != null) ? JObject.Parse(merged.JsonInfo): new JObject();

            foreach (UserInfoDetail uiDet in userInfos.Where(x => x.NetworkId != merged.NetworkId))
            {
                JObject toMergeJson = (uiDet != null && uiDet.JsonInfo != null) ? JObject.Parse(uiDet.JsonInfo): null;
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
                merged.JsonInfo = mergedJson.ToString();
            }
            return merged;
        }

    }
}
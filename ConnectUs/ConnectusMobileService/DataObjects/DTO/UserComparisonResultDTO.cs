using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO
{
    public class UserComparisonResultDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string EqualData { get; set; }
        public bool CurrentUserWasInitiator { get; set; }
        public string ProfilePic { get; set; }
    }
}
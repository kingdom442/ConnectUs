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
        public string CompUserId { get; set; }
        public string CompUserName { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string EqualData { get; set; }
    }
}
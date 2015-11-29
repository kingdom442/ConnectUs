using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.DTO
{
    public class SyncInconsistentProfileRequest
    {
        public string accountid { get; set; }
        public int provider { get; set; }
    }
}
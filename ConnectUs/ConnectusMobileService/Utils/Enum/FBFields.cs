using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Profile
{
    public enum FBFields
    {
        ABOUT, BIO, EMAIL, FIRST_NAME, LAST_NAME, RELATIONSHIP_STATUS, GENDER, AGE_RANGE, EDUCATION, FAVORITE_ATHLETES, FAVORITE_TEAMS, HOMETOWN, LANGUAGES, SPORTS, WORK,
        [Description("picture")]
        PICTURE, LINK, MUSIC
    }
}
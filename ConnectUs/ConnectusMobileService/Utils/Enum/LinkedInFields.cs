using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ConnectusMobileService.DataObjects.Profile
{
    public enum LinkedInFields
    {
        [Description("Interests")]
        INTERESTS,
        [Description("Educations")]
        EDUCATIONS,
        [Description("Skills")]
        SKILLS,
        [Description("EmailAddress")]
        EMAILADDRESS,
        [Description("FirstName")]
        FIRSTNAME,
        [Description("LastName")]
        LASTNAME,
        [Description("Headline")]
        HEADLINE,
        [Description("PictureUrl")]
        PICTUREURL,
        [Description("PublicProfileUrl")]
        PUBLICPROFILEURL,
        [Description("Industry")]
        INDUSTRY,
        [Description("Summary")]
        SUMMARY,
        [Description("Positions")]
        POSITIONS
    }
}
using System;

namespace ConnectusMobileService.DataObjects.DTO
{
    public class BasicUserInfoDTO
    {
        public BasicUserInfoDTO(string accountid, string username, string age, string status, string about, DateTimeOffset lastTimeOnline)
        {
            this.AccountId = accountid;
            this.Username = username;
            this.Age = age;
            this.Status = status;
            this.About = about;
            this.LastTimeOnline = lastTimeOnline;
        }

        public BasicUserInfoDTO()
        {
        }
        public string UserInfoId { get; set; }
        public string AccountId { get; set; }
        public string ProfilePicUrl { get; set; }
        public string Username { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string About { get; set; }
        public DateTimeOffset LastTimeOnline { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool FacebookConnected { get; set; }
        public bool LinkedInConnected { get; set; }
    }

    /// <summary>
    /// Basic info + contact information
    /// </summary>
    public class FullUserInfoDTO : BasicUserInfoDTO
    {
        public string PhoneNr { get; set; }
        public string Email { get; set; }
        public string FacebookProfileLink { get; set; }
        public string LinkedInProfileLink { get; set; }
    }
}
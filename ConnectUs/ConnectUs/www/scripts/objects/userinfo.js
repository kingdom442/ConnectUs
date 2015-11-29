var UserInfo = function UserInfo(accountId, username, profilePicUrl, status, about, gender, age, lasttimeonline, fbConnected, firstname, lastname, phoneNr, eMail, fbLink, userInfoId, linkedInConnected) {
    this.accountId = accountId;
    this.username = username;
    this.profilePicUrl = profilePicUrl;
    this.status = status;
    this.about = about;
    this.gender = gender;
    if (age)
        this.age = age;
    else
        this.age = '-';
    this.fbConnected = fbConnected;
    this.linkedInConnected = linkedInConnected;
    this.fbLink = fbLink;
    this.userContact = new UserContact(firstname, lastname, phoneNr, eMail);
    this.userInfoId = userInfoId;
}


var UserContact = function UserContact(firstname, lastname, phoneNr, eMail) {
    this.firstname = firstname;
    this.lastname = lastname;
    this.phoneNumber = phoneNr;
    this.eMail = eMail;
}

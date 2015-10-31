var UserInfo = function UserInfo(userid, username, profilePicUrl, description, gender, age, lasttimeonline, fbConnected, firstname, lastname, phoneNr, eMail, fbLink){
    this.userid = userid;
    this.username = username;
    this.profilePicUrl = profilePicUrl;
    this.description = description;
    this.gender = gender;
    if (age)
        this.age = age;
    else
        this.age = '-';
    this.fbConnected = fbConnected;
    this.linkedInConnected = false;
    this.fbLink = fbLink;
    this.userContact = new UserContact(firstname, lastname, phoneNr, eMail);
}


var UserContact = function UserContact(firstname, lastname, phoneNr, eMail) {
    this.firstname = firstname;
    this.lastname = lastname;
    this.phoneNumber = phoneNr;
    this.eMail = eMail;
}

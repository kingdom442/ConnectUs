var UserInfo = function UserInfo(userid, username, profilepicurl, description, gender, age, lasttimeonline, fbConnected, firstname, lastname){
    this.userid = userid;
    this.username = username;
    if(profilepicurl)
        this.profilepicurl = profilepicurl;
    else
        this.profilepicurl = 'images/profile-icon.png';
    this.description = description;
    this.gender = gender;
    if (age)
        this.age = age;
    else
        this.age = '-';
    this.fbConnected = fbConnected;
    this.linkedInConnected = false;
    this.firstname = firstname;
    this.lastname = lastname;
}

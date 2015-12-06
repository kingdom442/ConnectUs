// http://go.microsoft.com/fwlink/?LinkID=290993&clcid=0x407


angular.module('connectusApp').service('userService', function ($rootScope) {
    this.changeAccountInfo = function (accountId, username, businessInterested, privateInterested, succCB, errCB) {
        accountTable().update({
            "id": accountId,
            "Username": username,
            "BusinessInterest": businessInterested,
            "PrivateInterest": privateInterested
        }).done(function (result) {
            succCB();
        }, function (err) {
            errCB();
        });
    };

    this.loadUser = function (accountId, succCB, errCB) {
        accountTable().lookup(accountId).done(function (result) {
            succCB(result);
        }, function (err) {
            errCB();
        });
    };

    this.updateUserInfo = function (userinfo, sucCB, errCB) {
        if (!(userinfo instanceof UserInfo)) {
            console.error('wrong type: userinfo expected');
            return;
        }
        userInfoTable().lookup(userinfo.userInfoId).done(function (results) {
            if (!results.id)
                insertUserInfo(userinfo, sucCB, errCB);
            else
                updateUserInfo(userinfo, results.id, sucCB, errCB);
        }, function (err) {
            console.error("Error: " + err);
            insertUserInfo(userinfo, sucCB, errCB);
        });
    };

    function insertUserInfo(userinfo, sucCB, errCB) {
        connectusClient.getTable('UserInfo').insert({
            "UserId": userinfo.accountId,
            "Bio": userinfo.status,
            "About": userinfo.about,
            "Age": userinfo.age,
            "FirstName": userinfo.userContact.firstname,
            "LastName": userinfo.userContact.lastname
        }).done(function (result) {
            sucCB(result);
        }, function (err) {
            console.error(err);
        });
    }

    function updateUserInfo(userinfo, id, sucCB, errCB) {
        connectusClient.getTable('UserInfo').update({
            "id": id,
            "Age": userinfo.age,
            "Bio": userinfo.status,
            "About": userinfo.about,
            "FirstName": userinfo.userContact.firstname,
            "LastName": userinfo.userContact.lastname
        }).done(function (result) {
            sucCB(result);
        }, function (err) {
            console.error(err);
            errCB(err);
        });
    }

    this.syncFBUserInfo = function (successCB, errCB) {
        connectusClient.invokeApi('FBSyncProfile', {
            method: 'POST',
            body: { "accountid": $rootScope.accountId }
        }).done(function (results) {
            successCB();
        }, function (error) {
            errCB(error);
        });
    }

    this.syncInconsistentProfile = function (succCB, errCB) {
        connectusClient.invokeApi('SyncInconsistentProfile', {
            method: 'POST',
            body: {
                "accountid": $rootScope.accountId,
                "provider": 0
            }
        }).done(function (results) {
            succCB();
        }, function (error) {
            errCB();
        });
    }

    this.loadUserInfo = function (userId, successCB, errCB) {
        connectusClient.invokeApi('GetUserInfo', {
            method: 'POST',
            body: { "accountid": userId }
        }).done(function (response) {
            var userinfo = response.result;
            successCB(new UserInfo(userinfo.accountId, userinfo.username, userinfo.profilePicUrl, userinfo.status, userinfo.about, userinfo.gender,
                userinfo.age, null, userinfo.facebookConnected, userinfo.firstName, userinfo.lastName, userinfo.phoneNr, userinfo.email, userinfo.facebookProfileLink, userinfo.userInfoId, userinfo.linkedInConnected, userinfo.linkedInProfileLink, userinfo.businessInterested, userinfo.privateInterested));
        }, function (error) {
            console.error(error);
            errCB(error);
        });
    }

    

    this.updateUserContact = function (userinfo, sucCB, errCB) {
        if (!(userinfo instanceof UserInfo)) {
            console.error('wrong type: userinfo expected');
            return;
        }

        userContactTable().where({ "UserId": $rootScope.accountId }).read().done(function (results) {
            if (results.length > 0)
                updateUserContact(userinfo, results[0].id, sucCB, errCB);
            else
                insertUserContact(userinfo, sucCB, errCB);
        }, function (err) {
            insertUserContact(userinfo, sucCB, errCB);
        });
    };

    function insertUserContact(userinfo, sucCB, errCB) {
        userContactTable().insert({
            "UserId": userinfo.accountId,
            "PhoneNr": userinfo.userContact.phoneNumber,
            "Email": userinfo.userContact.eMail
        }).done(function (result) {
            sucCB(result);
        }, function (err) {
            console.error(err);
        });
    }

    function updateUserContact(userinfo, id, sucCB, errCB) {
        userContactTable().update({
            "id": id,
            "PhoneNr": userinfo.userContact.phoneNumber,
            "Email": userinfo.userContact.eMail
        }).done(function (result) {
            sucCB(result);
        }, function (err) {
            console.error(err);
            errCB(err);
        });
    }


});

var connectusClient;

function initMobileServiceClient() {
    providerLoginClient = new WindowsAzure.MobileServiceClient(
                    "https://connectus.azure-mobile.net/",
                    "KyGrSBAbWCFNuinSZRcuDgMqisxyDl59");
    connectusClient = new WindowsAzure.MobileServiceClient(
                "https://connectus.azure-mobile.net/",
                "KyGrSBAbWCFNuinSZRcuDgMqisxyDl59");

    //connectusClient = new WindowsAzure.MobileServiceClient(
    //            "http://192.168.135.131:62028/",
    //            "blunznkoenig");

    //connectusClient = new WindowsAzure.MobileServiceClient(
    //            "http://:62028/",
    //            "blunznkoenig");
    //https://localhost:44300/
}

function accountTable() {
    return connectusClient.getTable("User");
}

function userInfoTable() {
    return connectusClient.getTable("UserInfo");
}

function userContactTable() {
    return connectusClient.getTable("UserContact");
}

function userComparisonTable() {
    return connectusClient.getTable('UserComparison');
}

function connectTable() {
    return connectusClient.getTable("ConnectRequest");
}

function eventTable() {
    return connectusClient.getTable("Event");
}

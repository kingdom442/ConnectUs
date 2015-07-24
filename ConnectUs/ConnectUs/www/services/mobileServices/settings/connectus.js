// http://go.microsoft.com/fwlink/?LinkID=290993&clcid=0x407
var connectusClient;
document.addEventListener("deviceready", function () {
    connectusClient = new WindowsAzure.MobileServiceClient(
                    "https://connectus.azure-mobile.net/",
                    "KyGrSBAbWCFNuinSZRcuDgMqisxyDl59");
});
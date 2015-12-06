
function showAlert(msg, title) {
    ons.notification.alert({
        message: msg,
        // or messageHTML: '<div>Message in HTML</div>',
        title: title,
        buttonLabel: 'OK',
        animation: 'default', // or 'none'
        // modifier: 'optional-modifier'
        callback: function () {
            // Alert button is closed!
        }
    });
}

function problemInfo(infoTxt) {

}
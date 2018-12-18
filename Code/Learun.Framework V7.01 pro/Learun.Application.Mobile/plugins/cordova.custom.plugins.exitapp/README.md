cordova-plugin-exitapp
----------------------

This plugin adds the ability to programmatically close an app on Android or Windows Phone 8. 

## Installation

Plugin id: cordova.custom.plugins.exitapp

To install this plugin, follow the [Command-line Interface Guide](http://cordova.apache.org/docs/en/edge/guide_cli_index.md.html#The%20Command-line%20Interface).

If you are not using the Cordova Command-line Interface, follow [Using Plugman to Manage Plugins](http://cordova.apache.org/docs/en/edge/plugin_ref_plugman.md.html).

## Usage

confirmed = function(buttonIndex) {
    if(buttonIndex == 1) {
        console.log("navigator.app.exitApp");
        navigator.app.exitApp();
    } 
}

onTouch = function() {
    navigator.notification.confirm('', confirmed, 'Exit?');
}

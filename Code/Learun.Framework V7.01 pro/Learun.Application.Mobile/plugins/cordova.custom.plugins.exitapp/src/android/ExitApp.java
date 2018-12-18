package cordova.custom.plugins.exitapp;

import org.apache.cordova.CallbackContext;
import org.apache.cordova.CordovaInterface;
import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.CordovaWebView;
import org.apache.cordova.PluginResult;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;

public class ExitApp extends CordovaPlugin {
	protected void pluginInitialize() {}

	public boolean execute(String action, JSONArray args, CallbackContext callbackContext) throws JSONException {
		/*
		 * Finishes the activity provided by CordovaInterface.
		 */
		
		if (action.equals("exitApp")) {
			try {
				Activity activity = this.cordova.getActivity();
				activity.finish();
				callbackContext.sendPluginResult(new PluginResult(PluginResult.Status.OK, 0));
			} catch (Exception e) {
				callbackContext.sendPluginResult(new PluginResult(PluginResult.Status.ERROR, 1));
			}
			return true;
		}
		return false;
	}
}
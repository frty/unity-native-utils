# Unity native Utils

This plugin allows to open native platform sharing dialog and email composer with attached image on iOS and Android

## setup
Import unity package into your project

### iOS
In unity editor select file NativeUtils.mm in Plugins/iOS folder and checkmark dependency framework "MessageUI".

### Android
Open AndroidManifest file in Plugins/Android/NativeUtils/ folder and replace `YOUR_BUNDLE_ID` with app bundle id. 
```xml
 		<provider
            android:name="android.support.v4.content.FileProvider"
            android:authorities="YOUR_BUNDLE_ID.fileprovider"
            android:exported="false"
            android:grantUriPermissions="true" >
            <meta-data
                android:name="android.support.FILE_PROVIDER_PATHS"
                android:resource="@xml/file_paths" />
        </provider>
```
Minimum Api 15.

Build with Unity 5.4.3f1. 
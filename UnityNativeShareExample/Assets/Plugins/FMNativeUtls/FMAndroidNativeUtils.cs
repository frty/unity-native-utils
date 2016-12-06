using UnityEngine;

public class FMAndroidNativeUtils : FMNativeUtils
{
    private AndroidJavaObject utilsPlugin;

    public FMAndroidNativeUtils ()
    {
        AndroidJavaClass pluginClass = new AndroidJavaClass ("fmp.androidutils.AndroidUtilsPlugin");
        utilsPlugin = pluginClass.CallStatic<AndroidJavaObject> ("getInstance");
        AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
        utilsPlugin.Call ("setActivity", unityActivity);
    }

    public override void OpenShareDialog (string body, string subject, Texture2D texture)
    {
        //byte [] bytes = texture.EncodeToPNG ();
        //string fileName = "FMTestScreenShoot_internal.png";
        //string filePath = Application.persistentDataPath + "/" + fileName;
        //File.WriteAllBytes (filePath, bytes);
        InternalShareMedia (body, subject, SaveTexture2DToLocalStorage(texture));
    }

    public override void OpenShareDialog (string body, string subject, string localImagePath)
    {
        InternalShareMedia (body, subject, localImagePath);
    }

    public override void SendEmail (string subject, string body, string recipient, Texture2D texture)
    {
        InternalSendEmail (recipient, subject, body, SaveTexture2DToLocalStorage(texture));
    }

    public override void SendEmail (string subject, string body, string recipient, string imagePath)
    {
        InternalSendEmail (recipient, subject, body, imagePath);
    }

    private void InternalSendEmail (string recipient, string subject, string body, string imagePath)
    {
        utilsPlugin.Call ("SendEmail", recipient, subject, body, imagePath);
    }

    private void InternalShareMedia (string body, string subject, string imagePath)
    {
        utilsPlugin.Call ("ShareMedia", body, subject, imagePath);
    }

}

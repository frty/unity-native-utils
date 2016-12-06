using UnityEngine;
using System;
using System.IO;

public abstract class FMNativeUtils
{
    private static FMNativeUtils instance;

    public static FMNativeUtils Instance {
        get {
            if (instance == null) {
#if UNITY_ANDROID && !UNITY_EDITOR
                instance = new FMAndroidNativeUtils ();
#elif UNITY_IOS && !UNITY_EDITOR
                instance = new FMiOSNativeUtils();
#else
                instance = new FMNativeUtilsEditorStub ();
#endif
            }
            return instance;
        }
    }

    private const string SHARING_TEXTURE_NAME = "screenshot.png";

    public abstract void SendEmail (string subject, string body, string recipient, string imagePath);
    public abstract void SendEmail (string subject, string body, string recipient, Texture2D texture);
    public void SendEmail (string subject, string body, string recepient)
    {
        SendEmail (subject, body, recepient, (Texture2D)null);
    }

    public abstract void OpenShareDialog (string body, string subject, string localImagePath);
    public abstract void OpenShareDialog (string body, string subject, Texture2D texture);
    public void OpenShareDialog (string body, string subject)
    {
        OpenShareDialog (body, subject, (Texture2D)null);
    }

    internal static string SaveTexture2DToLocalStorage (Texture2D texture)
    {
        if (texture == null)
            return "";
        
        string path = Application.persistentDataPath + "/" + SHARING_TEXTURE_NAME;
        byte [] data = texture.EncodeToPNG ();
        File.WriteAllBytes (path, data);
        return path;
    }

    internal static string ConvertTextureToString (Texture2D texture)
    {
        if (texture == null)
            return "";

        byte [] data = texture.EncodeToPNG ();
        string result = Convert.ToBase64String (data);
        return result;
    }

    internal static string ConvertTextureAtPathToString (string imagePath)
    {
        string result;
        byte [] data = File.ReadAllBytes (imagePath);
        Texture2D texture = new Texture2D (2, 2);
        texture.LoadImage (data);
        result = ConvertTextureToString (texture);
        texture = null;
        return result;
    }
}







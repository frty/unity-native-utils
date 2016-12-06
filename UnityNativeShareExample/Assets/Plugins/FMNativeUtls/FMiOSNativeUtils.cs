using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class FMiOSNativeUtils : FMNativeUtils
{
    public override void SendEmail (string subject, string body, string recipient, Texture2D texture)
    {
        sendEmail (subject, body, recipient, ConvertTextureToString (texture));
    }

    public override void SendEmail (string subject, string body, string recipient, string imagePath)
    {
        sendEmail (subject, body, recipient, ConvertTextureAtPathToString (imagePath));
    }

    public override void OpenShareDialog (string body, string subject, string localImagePath)
    {
        showSocialSharing (body, "", ConvertTextureAtPathToString (localImagePath), subject);
    }

    public override void OpenShareDialog (string body, string subject, Texture2D texture)
    {
        showSocialSharing (body, "", ConvertTextureToString (texture),subject);
    }

    [DllImport ("__Internal")]
    private static extern void showSocialSharing (string body, string url, string imagePath, string subject);

    [DllImport ("__Internal")]
    private static extern void sendEmail (string subject, string body, string recepient, string imagePath);
}

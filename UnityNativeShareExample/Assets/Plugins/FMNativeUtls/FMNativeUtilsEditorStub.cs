using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class FMNativeUtilsEditorStub : FMNativeUtils
{
    public override void OpenShareDialog (string body, string subject, Texture2D texture)
    {
        Debug.Log ("OpenShareDialog editor stub!");
    }

    public override void OpenShareDialog (string body, string subject, string localImagePath)
    {
        Debug.Log ("OpenShareDialog editor stub!");
    }

    public override void SendEmail (string subject, string body, string recipient, Texture2D texture)
    {
        Debug.Log ("SendEmail editor stub!");
    }

    public override void SendEmail (string subject, string body, string recipient, string imagePath)
    {
        Debug.Log ("SendEmail editor stub!");
    }
}

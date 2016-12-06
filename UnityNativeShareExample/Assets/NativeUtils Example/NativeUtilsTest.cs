using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class NativeUtilsTest : MonoBehaviour
{
    public RawImage image;
    private Texture2D texture2D;
    string path;

    readonly string url = "https://github.com/frty/unity-native-utils";
    readonly string email = "a@a.com";


    void Start ()
    {
        texture2D = GenerateRandom (50);
        image.texture = texture2D;
        path = SaveTexture (texture2D);
    }

    // Update is called once per frame
    void Update ()
    {
    }

    void OnGUI ()
    {
        if (GUI.Button (new Rect (0, 0, Screen.width / 3, 100), "share url only")) {
            FMNativeUtils.Instance.OpenShareDialog (url, "my subject");
        }

        if (GUI.Button (new Rect (Screen.width / 3, 0, Screen.width / 3, 100), "share text with subject")) {
            FMNativeUtils.Instance.OpenShareDialog ("my body " + Environment.NewLine + "new line", "my subject");
        }

        if (GUI.Button (new Rect (Screen.width * 2 / 3, 0, Screen.width / 3, 100), "share text with url and subject")) {
            FMNativeUtils.Instance.OpenShareDialog ("my body " + Environment.NewLine + "new line " + Environment.NewLine + url, "my subject");
        }

        if (GUI.Button (new Rect (0, 100, Screen.width / 3, 100), "share img by path")) {
            FMNativeUtils.Instance.OpenShareDialog ("", "", path);
        }

        if (GUI.Button (new Rect (Screen.width / 3, 100, Screen.width / 3, 100), "share texture2D")) {

            FMNativeUtils.Instance.OpenShareDialog ("", "", texture2D);
        }

        if (GUI.Button (new Rect (Screen.width * 2 / 3, 100, Screen.width / 3, 100), "share img with text and subject")) {

            FMNativeUtils.Instance.OpenShareDialog (url, "my subject", texture2D);
        }

        if (GUI.Button (new Rect (0, 200, Screen.width / 3, 100), "send email no pic")) {
            FMNativeUtils.Instance.SendEmail ("my subject", "my body " + Environment.NewLine + "second line", email);
        }

        if (GUI.Button (new Rect (Screen.width / 3, 200, Screen.width / 3, 100), "send email with img by path")) {
            FMNativeUtils.Instance.SendEmail ("my subject", "my body", email, path);
        }

        if (GUI.Button (new Rect (Screen.width * 2 / 3, 200, Screen.width / 3, 100), "send email with texture2d")) {
            FMNativeUtils.Instance.SendEmail ("my subject", "my body", email, texture2D);
        }

        if (GUI.Button (new Rect (0, Screen.height - 100, Screen.width / 2, 100), "Generate texture")) {
            if (texture2D != null)
                Destroy (texture2D);
            texture2D = GenerateRandom (20);
            image.texture = texture2D;
        }

        if (GUI.Button (new Rect (Screen.width / 2, Screen.height - 100, Screen.width / 2, 100), "Save texture")) {
            if (texture2D != null) {
                path = SaveTexture (texture2D);
            }
        }
    }

    private string CaptureScreenShotString ()
    {
        string fileName = "FMTestScreenShoot.png";
        string path = Application.persistentDataPath + "/" + fileName;
        Application.CaptureScreenshot (fileName);
        return path;
    }

    private Texture2D CaptureScreenShotTexture ()
    {
        string filePath = CaptureScreenShotString ();
        Texture2D tex = null;
        byte [] fileData;

        if (File.Exists (filePath)) {
            fileData = File.ReadAllBytes (filePath);
            tex = new Texture2D (2, 2);
            tex.LoadImage (fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    private Texture2D GenerateRandom (int size)
    {
        Texture2D texture = new Texture2D (size, size);

        Color [] colors = new Color [size * size];

        float startColor = Random.Range (0f, 1f);
        float startColor2 = Random.Range (0f, 1f);

        for (int i = 0; i < colors.Length; i++) 
        {
            colors [i] = new Color (startColor, startColor2, Random.Range (0f, 1f));
            //colors [i] = new Color (Random.Range(startColor,1f), Random.Range (startColor2, 1f),Random.Range (0f, 1f));        
        }
        texture.SetPixels (colors);
        texture.Apply ();
        return texture;
    }

    private string SaveTexture (Texture2D texture)
    {
        byte [] bytes = texture.EncodeToPNG ();
        string fileName = "blabla.png";
        string filePath = Application.persistentDataPath + "/" + fileName;
        File.WriteAllBytes (filePath, bytes);
        return filePath;
    }
}

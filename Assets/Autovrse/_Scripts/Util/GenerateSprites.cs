using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateSprites : MonoBehaviour
{
    [ContextMenu("TakeScreenshot")]
    public void TakeScreenshots()
    {
        Camera camera = Camera.main;

        RenderTexture rt = new RenderTexture(256, 256, 24);
        camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        DestroyImmediate(rt);

        byte[] byteData = screenshot.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.dataPath, "image.png"), byteData);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}

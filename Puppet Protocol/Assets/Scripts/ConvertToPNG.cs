using System.IO;
using UnityEngine;

public class ConvertToPNG : MonoBehaviour
{
    public RenderTexture rtex;

    // Start is called before the first frame update
    void Start()
    {
    }

    float time = 0;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 10)
        {
            time = -1000;
            snap();
        }
    }

    void snap()
    {
        var oldRT = RenderTexture.active;

        var tex = new Texture2D(rtex.width, rtex.height);
        RenderTexture.active = rtex;
        tex.ReadPixels(new Rect(0, 0, rtex.width, rtex.height), 0, 0);
        tex.Apply();

        File.WriteAllBytes("C:\\git\\file.png", tex.EncodeToPNG());
        RenderTexture.active = oldRT;
        Debug.Log("SNAP");
    }
}

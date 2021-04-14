using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PictureFrame : MonoBehaviour
{
    public RawImage image;
    public Text title;
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePictureFrame(string url)
    {
        this.url = url;
        StartCoroutine(SetImage(url));
    }

    IEnumerator SetImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    }
}

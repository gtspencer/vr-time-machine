using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageUI : MonoBehaviour
{
    public RawImage image1;
    public RawImage image2;
    public RawImage image3;
    public RawImage image4;

    public Text indexLabel;

    public GameObject framePrefab;
    public Transform frameSpawnLocation;

    string blankImage;

    List<ImageFile> imageFiles;
    List<RawImage> rawImages;
    int currentPage;
    int totalPages;
    int remainder;
    // Start is called before the first frame update
    void Start()
    {
        rawImages = new List<RawImage>();
        rawImages.Add(image1);
        rawImages.Add(image2);
        rawImages.Add(image3);
        rawImages.Add(image4);
        blankImage = Application.dataPath + "/blankImage.png";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNewImages(List<ImageFile> files)
    {
        imageFiles = files;
        currentPage = 1;
        totalPages = (imageFiles.Count / 4) + 1;
        remainder = imageFiles.Count % 4;
        Debug.LogError($"total index {totalPages}");
        SetImagesToScreen();
    }

    public void NextImages()
    {
        if (imageFiles == null)
            return;
        currentPage++;
        if (currentPage > totalPages)
            currentPage = totalPages;
        SetImagesToScreen();
    }

    public void PreviousImages()
    {
        if (imageFiles == null)
            return;
        currentPage--;
        if (currentPage < 1)
            currentPage = 1;
        SetImagesToScreen();
    }

    public void SetImagesToScreen()
    {
        if (currentPage == totalPages)
        {
            int rawImageIndex = 0;
            for (int i = (currentPage * 4) - 4; i < imageFiles.Count; i++)
            {
                StartCoroutine(SetImage(imageFiles[i].url, rawImages[rawImageIndex]));
                rawImageIndex++;
            }
            for (int i = rawImageIndex; i < rawImages.Count; i++)
            {
                StartCoroutine(SetImage(blankImage, rawImages[i]));
            }
        } else
        {
            StartCoroutine(SetImage(imageFiles[(currentPage * 4) - 4].url, image1));
            StartCoroutine(SetImage(imageFiles[(currentPage * 4) - 3].url, image2));
            StartCoroutine(SetImage(imageFiles[(currentPage * 4) - 2].url, image3));
            StartCoroutine(SetImage(imageFiles[(currentPage * 4) - 1].url, image4));
            /*int imageIndex = currentPage * 4;
            for (int i = imageIndex; i < imageIndex + 4; i++)
            {
                StartCoroutine(SetImage(imageFiles[i].url, rawImages[rawImageIndex]));
                rawImageIndex++;
            }*/
        }
        indexLabel.text = $"{currentPage}/{totalPages}";
    }

    IEnumerator SetImage(string url, RawImage rawImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    }

    public void MakeImage1Frame()
    {
        Instantiate(framePrefab, frameSpawnLocation).GetComponentInChildren<PictureFrame>().CreatePictureFrame(imageFiles[(currentPage * 4) - 4].url);
    }

    public void MakeImage2Frame()
    {
        Instantiate(framePrefab, frameSpawnLocation).GetComponentInChildren<PictureFrame>().CreatePictureFrame(imageFiles[(currentPage * 4) - 3].url);
    }

    public void MakeImage3Frame()
    {
        Instantiate(framePrefab, frameSpawnLocation).GetComponentInChildren<PictureFrame>().CreatePictureFrame(imageFiles[(currentPage * 4) - 2].url);
    }

    public void MakeImage4Frame()
    {
        Instantiate(framePrefab, frameSpawnLocation).GetComponentInChildren<PictureFrame>().CreatePictureFrame(imageFiles[(currentPage * 4) - 1].url);
    }
}

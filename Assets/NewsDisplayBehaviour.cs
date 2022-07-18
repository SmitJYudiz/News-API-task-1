using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NewsDisplayBehaviour : MonoBehaviour
{
    public TextMeshProUGUI _authorName;
    public TextMeshProUGUI _title;
    public TextMeshProUGUI _publishTime;
    public TextMeshProUGUI _description;
    

    public RawImage _articleImage;

    public static NewsDisplayBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

   

    public void SetNewsDetails(string authorName, string title, string publishTime, string description, string imageUrl)
    {
        _authorName.text = authorName;
        _title.text = title;
        _publishTime.text = publishTime;
        _description.text = description;

        StartCoroutine(SetRawImageFromUrl(imageUrl));
    }


    IEnumerator SetRawImageFromUrl(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        _articleImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        request.Dispose();
    }
}

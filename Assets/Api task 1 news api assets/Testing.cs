using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

using System;

public class Testing : MonoBehaviour
{

    public TextMeshProUGUI textMesh;

    //smit's work
    private void Start()
    {
        string url = "https://google.com";

        Get(url, (string error)=> {//Error
            Debug.Log("Error: " + error);
            textMesh.SetText("Error: " + error);
                 },
            (string text) => { //successfully contacted url
                Debug.Log("Recieved: " + text);
                textMesh.SetText("Recieved: " + text);
            });
    }

    //using delegates: so we can define some actions like success and failures:


    //making it easy to use a coroutine by putting it inside a method:
    private void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }


    private IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if(unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError|| unityWebRequest.result == UnityWebRequest.Result.DataProcessingError)
            //if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                //Debug.Log("Error: " + unityWebRequest.error);
                textMesh.SetText("Error: " + unityWebRequest.error);
                onError(unityWebRequest.error);
            }
            else
            {
                //Debug.Log("Recieved: " + unityWebRequest.downloadHandler.text);
                //textMesh.SetText("Recieved: " + unityWebRequest.downloadHandler.text);
                onSuccess(unityWebRequest.downloadHandler.text);


            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//using UnityEngine.JSONSerializeModule;


public class GetUrlBehaviour : MonoBehaviour
{
    //access a website and use UnityWebRequest.Get to downlaod a page.

    //also try to download a non-existing page. Display the error.

    private void Start()
    {
        StartCoroutine(GetRequest("https://newsapi.org/"));

        //a correct websit page:
        //StartCoroutine(GetRequest("https://www.example.com"));

        //a non-existing page:
        // StartCoroutine(GetRequest("http://error.html"));
    }

    IEnumerator GetRequest(string url)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            //Request and wait for the desired page

            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');

            int page = pages.Length - 1;

            switch(webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nRecieved: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

}




//public class GetUrlBehaviour : MonoBehaviour
//{
//    private void Start()
//    {
//        string url = "http://example.com/script.php?var1=value2&amp;var2=value2";

//        UnityWebRequest www = new UnityWebRequest(url);

//        StartCoroutine(WaitForRequest(www));

//    }

//    IEnumerator WaitForRequest(UnityWebRequest www)
//    {
//        yield return www;

//        //checking for errors, if any:
//        if(www.error == null)
//        {
//            //errors are null, means no error:
//            Debug.Log("WWW ok! : " + www.result);
//        }
//        else
//        {
//            Debug.Log("WWW error: " + www.error);
//        }
//    }
//}


//api key:  7d6d9f094cc14f0ab45d0e4a6172d061
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class GetNewsFromUrlBehaviour : MonoBehaviour
{
    public  GameObject VerticalGroup1;
    public  GameObject VerticalGroup2;

    public Transform HorizontalTabsGroup;

    public RawImage rawImage;

    public static GetNewsFromUrlBehaviour instance;

    float numberOfArticles;

    public List<HeadlineButtonBehaviour> headlineButtonsList;

    float delayToLoadHeadlineforFirstTime;
    bool loadedHeadlinesForFirstTime = false;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        StartCoroutine(Read());

        delayToLoadHeadlineforFirstTime = 0.3f;

        //Debug.Log(numberOfArticles);


        ScreenManager.instance.ShowAllHeadlines();


        //SetHeadlines(1);
    }


    private void Update()
    {
        delayToLoadHeadlineforFirstTime -= Time.deltaTime;
        if(delayToLoadHeadlineforFirstTime<=0 && loadedHeadlinesForFirstTime == false)
        {
            SetHeadlines(0);
            loadedHeadlinesForFirstTime = true;
        }
    }


    public Root root;


    IEnumerator Read()
    {
        //below url have 100 articles:
        UnityWebRequest url = UnityWebRequest.Get("https://newsapi.org/v2/everything?q=apple&from=2022-07-12&to=2022-07-12&sortBy=popularity&apiKey=e1fa14bc9882454cb387e335cc2abee0");

        //below url have 20 articles:
        //UnityWebRequest url = UnityWebRequest.Get("https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey=7d6d9f094cc14f0ab45d0e4a6172d061");

        yield return url.SendWebRequest();

        if (url.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("---------something wrong with internet---------");
        }

        else
        {
            string result = url.downloadHandler.text;
            root = JsonUtility.FromJson<Root>(result);

            numberOfArticles = root.articles.Count;

            float numberOfPagesToDisplay = numberOfArticles / 10;
            //float numberOfPagesToDisplay = Mathf.Ceil(numberOfArticles / 10);
            Debug.Log("number of articles: " + numberOfArticles + "     number Of Pages" + numberOfPagesToDisplay);

            for (int i = 0; i < 10; i++)
            {
                if (i < numberOfPagesToDisplay)
                {
                    HorizontalTabsGroup.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    HorizontalTabsGroup.GetChild(i).gameObject.SetActive(false);
                }


            }

            url.Dispose();
        }
    }

    [System.Serializable]
    public class Article
    {
        public Source source;
        public string author;
        public string title;
        public string description;
        public string url;
        public string urlToImage;
        //changed below line
        public string publishedAt;
        public string content;
    }

    [System.Serializable]
    public class Root
    {
        public string status;
        public int totalResults;
        public List<Article> articles;
    }

    [System.Serializable]
    public class Source
    {
        public string id;
        public string name;
    }


    public  void SetHeadlines(int pageIndex)       //from 0 to 9... e.g if pageindex = 0: then headlines 0*10 = 0 to 9... if pageindex = 3: then headline 21 to 30
    {
        
        int iteratorStart = (pageIndex)*10;
        int iteratorEnd = iteratorStart + 10;



        int counter = 0;
        while (iteratorStart<iteratorEnd)
        {
           
            if (counter < 5)
            {
                TextMeshProUGUI currentTextBox = VerticalGroup1.transform.GetChild(counter).GetChild(0).GetComponent<TextMeshProUGUI>();
                if (currentTextBox != null)
                {
                    currentTextBox.text = root.articles[iteratorStart].title;
                    
                    StartCoroutine(GetRawImage(iteratorStart, counter, VerticalGroup1.transform));
                }
                else
                {
                    Debug.Log("something wrong with the textbox you are trying to reach");
                }
            }
            else
            {
                TextMeshProUGUI currentTextBox = VerticalGroup2.transform.GetChild(counter-5).GetChild(0).GetComponent<TextMeshProUGUI>();
                if (currentTextBox != null)
                {
                    currentTextBox.text = root.articles[iteratorStart].title;
                    
                    StartCoroutine(GetRawImage(iteratorStart, counter-5, VerticalGroup2.transform));
                }
                else
                {
                    Debug.Log("something wrong with the textbox you are trying to reach");
                }
            }

            headlineButtonsList[counter].headlineNumberOverall = iteratorStart;
            counter++;
            iteratorStart++;
        }
    }

    IEnumerator GetRawImage(int imageIndex, int childIndex, Transform verticalGroup)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(root.articles[imageIndex].urlToImage);
        yield return request.SendWebRequest();
        verticalGroup.transform.GetChild(childIndex).GetChild(1).GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;    

        request.Dispose();
    }

}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlineButtonBehaviour : MonoBehaviour
{
    public int headlineNumberOverall;
    GetNewsFromUrlBehaviour.Article article;

    //public GameObject newsDisplay;
    //public GameObject verticalgroup1;
    //public GameObject verticalgroup2;

    public Canvas allHeadlineCanvas;

    public Canvas newsArticleCanvas; 


    public void OnClickHeadline()
    {
        article  = GetNewsFromUrlBehaviour.instance.root.articles[headlineNumberOverall];
        NewsDisplayBehaviour.instance.SetNewsDetails(article.author, article.title, article.publishedAt, article.description, article.urlToImage);
        ScreenManager.instance.ShowArticleInfo();
    }
}

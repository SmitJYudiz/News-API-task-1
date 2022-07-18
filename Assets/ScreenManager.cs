using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public Canvas allHeadlinCanvas;
    public Canvas articleDisplayCanvas;

    public static ScreenManager instance;

    private void Awake()
    {
        instance = this;
    }

    
    public void ShowAllHeadlines()
    {
       
        articleDisplayCanvas.GetComponent<Canvas>().enabled = false;
        allHeadlinCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void ShowArticleInfo()
    {
        allHeadlinCanvas.GetComponent<Canvas>().enabled = false;
        articleDisplayCanvas.GetComponent<Canvas>().enabled = true;
    }
}

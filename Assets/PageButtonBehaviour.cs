using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButtonBehaviour : MonoBehaviour
{
    public int pageNumber;

    public void OnPageClick()
    {
        GetNewsFromUrlBehaviour.instance.SetHeadlines(pageNumber);
    }

}

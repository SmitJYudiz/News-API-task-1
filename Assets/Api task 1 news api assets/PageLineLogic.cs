using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageLineLogic : MonoBehaviour
{
    public void SetHeadlines(int pageIndex)
    {
        int iteratorStart = pageIndex * 10;
        int iteratorEnd = iteratorStart + 10; //(pageIndex + 1) * 10;
        int counter = 0;
        while(iteratorStart<iteratorEnd)
        {
            if(counter<5)
            {
                Debug.Log("child no: " + counter + " and article number:  " + iteratorStart);
                //first five headlines;
            }
            else
            {
                Debug.Log("child no: " + counter + " and article number:  " + iteratorStart);
            }

                iteratorStart++;
            counter++;
        }
    }

    private void Start()
    {
        SetHeadlines(3);
    }
}

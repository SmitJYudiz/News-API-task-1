using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllUserCanvasBehaviour : MonoBehaviour
{
    //smit's work

    public InputFormBehaviour formObject;


    public static AllUserCanvasBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject seeUserDetailByNameBtnObject;


    public List<GameObject> listOfUserButtons;

    public Transform verticalGroup;

    int currentNumberOfButtons;

    private void Start()
    {
        currentNumberOfButtons = 0;
    }

    public void SetUserButtons(int numberOfButtonsToCreate)
    {
        //delete all childs first...
        
        //formObject.root.userList.Count;
        for(int i=0; i<numberOfButtonsToCreate; i++)
        {
            if(i >= currentNumberOfButtons)
            {
                GameObject button = Instantiate(seeUserDetailByNameBtnObject, verticalGroup);

                //setting user id below:
                button.GetComponent<ShowSingleUserDetailButtonBehaviour>().userNumber = i;

                listOfUserButtons.Add(button);
                listOfUserButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = formObject.root.userList[i].name;
                currentNumberOfButtons++;
            }
           
        }       
    }
}

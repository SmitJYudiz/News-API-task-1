using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDataFromJsonBinSiteBehaviour : MonoBehaviour
{

    public InputFormBehaviour formObject;

    public InputFormBehaviour.Root root;

    public List<InputFormBehaviour.userData> userListNew;

    public static GetDataFromJsonBinSiteBehaviour instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
       
        //GetDataFromJson();
    }

    private void Update()
    {
        userListNew = formObject.GetLatestUserList();


        //Debug.Log("number of users: " + userListNew.Count);
        //Debug.Log("number of users: " + formObject.GetLatestUserList().Count);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetDataFromJson();
        }
       


       
    }

    public void GetDataFromJson()
    {
        //formObject.GetData();

        Debug.Log("user in userList: " + formObject.root.userList.Count);

        foreach(InputFormBehaviour.userData user in formObject.GetLatestUserList() )
        {
            Debug.Log(user.name);
        }
    }

    public void OnGetDataButtonClick()
    {
        AllUserCanvasBehaviour.instance.SetUserButtons(formObject.root.userList.Count);
        UserScreenManager.instance.ShowAllUsers();
    }

}

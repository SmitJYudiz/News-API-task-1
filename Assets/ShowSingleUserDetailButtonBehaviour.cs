using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class ShowSingleUserDetailButtonBehaviour : MonoBehaviour
{
    //smit's work

    public int userNumber;

    public void OnClickBehaviour()
    {
       
        SingleUserDetailCanvasBehaviour.instance.SetDataForSingleUser(GetDataFromJsonBinSiteBehaviour.instance.formObject.root.userList[userNumber]);
        UserScreenManager.instance.ShowSingleUserDetailsCanvas();
       
    }

    public void OnDeleteUserBtnClick()
    {
        //first delete that user from the list of our root

        //then update the list of buttons.

        GetDataFromJsonBinSiteBehaviour.instance.formObject.root.userList.RemoveAt(userNumber);

        AllUserCanvasBehaviour.instance.listOfUserButtons[userNumber].SetActive(false);

        string jsonData = JsonUtility.ToJson(GetDataFromJsonBinSiteBehaviour.instance.formObject.root);
        Debug.Log(jsonData);
        //  File.AppendAllText(Application.dataPath + "userListInJson.txt", jsonData);
        UnityWebRequest putData = UnityWebRequest.Put("https://api.jsonbin.io/v3/b/62d682ce4d5b061b1b584785", jsonData);

        putData.SetRequestHeader("Content-Type", "application/json");
        putData.SetRequestHeader("X-Master-Key", "$2b$10$Htsse58q4hsKeq9DbCt61uwG5q0se2q5qupXeJSDG8tihISlgimlm");
        putData.SendWebRequest();


        //GetDataFromJsonBinSiteBehaviour.instance.OnGetDataButtonClick();
    }
}

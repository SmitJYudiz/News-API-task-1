using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;
using SimpleJSON;



[System.Serializable]
public class InputFormBehaviour : MonoBehaviour
{
    
    public TMP_InputField nameInput;
    public TMP_InputField contactInput;
    public TMP_InputField passionInput;
    public TMP_InputField companyNameInput;
    public TMP_InputField emailInput;

    public Root root;

    private void Start()
    {
        
        //at starting of our playmode:, we first need to get the data from the jsonbin api site and pass it to the root, so now we are synchronised with the online data
        GetData();
    }
    public bool InputCheck()
    {
        if (nameInput.text == "")
        {
            Debug.Log("fill your name!");
            return false;
        }
        if (contactInput.text.Length!=10)
        {
            Debug.Log(" contact is of 10 digits, fill that properly!");
            return false;
        }
        if (passionInput.text == "")
        {
            Debug.Log("provide your pasison!");
            return false;
        }
        if (companyNameInput.text == "")
        {
            Debug.Log("provide your company name!");
            return false;
        }
        if (emailInput.text == "")
        {
            Debug.Log("provide your email!");
            return false;
        }
        return true;
    }


    public void SubmitUserDetails()
    {
        if(InputCheck())
        {
            userData user = new userData();
            user.name = nameInput.text;
            user.passion = passionInput.text;
            user.contact = long.Parse(contactInput.text);
            user.email = emailInput.text;
            user.companyName = companyNameInput.text;

            root.userList.Add(user);
            string jsonData = JsonUtility.ToJson(root);
            Debug.Log(jsonData);
            //  File.AppendAllText(Application.dataPath + "userListInJson.txt", jsonData);
            UnityWebRequest putData = UnityWebRequest.Put("https://api.jsonbin.io/v3/b/62d682ce4d5b061b1b584785", jsonData);
            
            putData.SetRequestHeader("Content-Type", "application/json");
            putData.SetRequestHeader("X-Master-Key", "$2b$10$Htsse58q4hsKeq9DbCt61uwG5q0se2q5qupXeJSDG8tihISlgimlm");
            putData.SendWebRequest();

            //here using url.Dispose make the project not working so not using that...
            //putData.Dispose();


            //ConvertUserListToJson(root.userData);
        }
    }

    public void GetData()
    {
        StartCoroutine(GetJsonData());
    }


    public void PutData()
    {
        //MakeSomeDummyData();
        StartCoroutine(PostJsonData());
    }


    IEnumerator GetJsonData()
    {
        

        UnityWebRequest url = UnityWebRequest.Get("https://api.jsonbin.io/v3/b/62d682ce4d5b061b1b584785");

    

        yield return url.SendWebRequest();

        if(url.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("something wrong with internet or url you are trying to rich");
        }
        else
        {
            Debug.Log("net success!");
            string jsonString = url.downloadHandler.text;
            var currentRoot = JSON.Parse(jsonString);
            root = JsonUtility.FromJson<Root>(currentRoot["record"].ToString());

            Debug.Log(root.userList.Count);           
        }

        url.Dispose();

    }


    //public void MakeSomeDummyData()
    //{
    //    UserDataClass user1 = new UserDataClass();
    //    user1.name = "abc";
    //    root.userData.Add(user1);

    //    UserDataClass user2 = new UserDataClass();
    //    user2.name = "def";
    //    root.userData.Add(user1);

    //    UserDataClass user3 = new UserDataClass();
    //    user3.name = "ghi";
    //    root.userData.Add(user1);

    //    UserDataClass user4 = new UserDataClass();
    //    user4.name = "jkl";
    //    root.userData.Add(user1);

    //    UserDataClass user5 = new UserDataClass();
    //    user5.name = "mno";
    //    root.userData.Add(user1);



    //}

   

    IEnumerator PostJsonData()
    {
        string jsonStringToPost = JsonUtility.ToJson(root);

        UnityWebRequest postUrl = UnityWebRequest.Put("https://api.jsonbin.io/v3/b/62d66f94b34ef41b73c89cad", jsonStringToPost);

        yield return postUrl.SendWebRequest();

        if(postUrl.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("cant upload the data");
        }
        else
        {
            Debug.Log("data posted succesfully");
        }

        postUrl.Dispose();
    }

    //public void ConvertUserListToJson(List<UserDataClass> userListData)
    //{
    //    string dataInJson = JsonUtility.ToJson(userListData);
    //    Debug.Log("Data in Json: " + dataInJson);
    //    WriteToJsonTextFile(dataInJson);
    //}


    //public void WriteToJsonTextFile(string dataInJson)
    //{
    //    File.WriteAllText(Application.dataPath + "/userListInJson.txt", dataInJson);
    //}

    [System.Serializable]
    public class userData
    {
        public string name;
        public string passion;
        public long contact;
        public string email;
        public string companyName;
    }

    [System.Serializable]
    public class Root
    {
        public List<userData> userList;
        //the name of the above list must be same as the one which is given in json bin list name inside records.
    }

    //public Root GetLatestRoot()
    //{
    //    GetData();
    //    return root;
    //}

    public List<userData> GetLatestUserList()
    {
        
        return root.userList;
    }
}



//SimpleJson library

//in json for ToJson method we always need to pass an object, and remember list is not an object, it is a collection type.

//there is a problem here, when add for new user and submit then old data of old user is deleted, what we now do is, append all old data and new data
//then make it to json post



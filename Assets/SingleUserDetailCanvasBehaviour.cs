using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleUserDetailCanvasBehaviour : MonoBehaviour
{
    //smit's work


    public static SingleUserDetailCanvasBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI contactTxt;
    public TextMeshProUGUI emailTxt;
    public TextMeshProUGUI passionTxt;
    public TextMeshProUGUI companyNameTxt;

    public void SetDataForSingleUser(InputFormBehaviour.userData user)
    {
        nameTxt.text = user.name;
        contactTxt.text = user.contact.ToString();
        emailTxt.text = user.email;
        passionTxt.text = user.passion;
        companyNameTxt.text = user.companyName;

    }



}

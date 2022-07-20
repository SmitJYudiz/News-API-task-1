using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScreenManager : MonoBehaviour
{
    //smit's work

    public static UserScreenManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Canvas allUserCanvas;

    public Canvas registrationCanvas;

    public Canvas singleUserDetailOutputCanvas;


    public void ShowAllUsers()
    {
        allUserCanvas.enabled = true;
        registrationCanvas.enabled = false;
        singleUserDetailOutputCanvas.enabled = false;
    }

    public void ShowSingleUserDetailsCanvas()
    {
        singleUserDetailOutputCanvas.enabled = true;
        allUserCanvas.enabled = false;
        registrationCanvas.enabled = false;
    }

    public void ShowRegistrationCanvas()
    {
        registrationCanvas.enabled = true;
        allUserCanvas.enabled = false;
        singleUserDetailOutputCanvas.enabled = false;
    }
}

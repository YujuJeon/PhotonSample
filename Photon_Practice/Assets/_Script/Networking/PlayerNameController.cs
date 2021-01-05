using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameController : MonoBehaviourPun
{
    private TextMeshProUGUI nameText;
    string trimText;

    // Start is called before the first frame update
    private void Start()
    {
        nameText = GetComponent<TextMeshProUGUI>();

        if (AuthManager.User != null)
        {
            trimText = AuthManager.User.Email;

            int index = trimText.IndexOf('@');
            if (index >= 0)
            {
                nameText.text = trimText.Substring(0, index);
            }
        }
        else
        {
            nameText.text = "Error : AuthManager.User == null";
        }    
    }    
}

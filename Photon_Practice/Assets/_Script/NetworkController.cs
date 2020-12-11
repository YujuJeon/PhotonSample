using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("Network Setup")]

    [Tooltip("The maximum number of players")]
    [SerializeField]
    private int maxPlayer;

    [Header("Unity Setup")]

    [Tooltip("The start button game object")]
    [SerializeField]
    private GameObject startButton;

    [Tooltip("The setting button game object")]
    [SerializeField]
    private GameObject settingButton;

    [Tooltip("The exit button game object")]
    [SerializeField]
    private GameObject exitButton;

    [Tooltip("The confirm button game object")]
    [SerializeField]
    private GameObject confirmButton;

    [Tooltip("Text to display the connection status")]
    [SerializeField]
    private TextMeshProUGUI connectionText;

    public GameObject startMenu;
    public GameObject settingMenu;



    // Start is called before the first frame update
    void Start()
    {
        connectionText.text = "Please press start button to start server";
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = false;
        }
    }

    public override void OnConnectedToMaster()
    {
        connectionText.text = "Connected to master server";
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void StartClick()
    {
        Connect();
        Debug.Log("Start button clicked");

        //needa disable setting and exit button
    }

    public void SettingClick()
    {
        Debug.Log("setting button clicked");

        startMenu.SetActive(false);
        settingMenu.SetActive(true);        
    }

    public void ConfirmClick()
    {
        Debug.Log("Confirm button clicked");

        startMenu.SetActive(true);
        settingMenu.SetActive(false);
    }

    public void ExitClick()
    {
        //connectionText.text = "Program Exit";
        Debug.Log("Exit button clicked");
        PhotonNetwork.Disconnect();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit(0);
        #endif
    }

    public void Connect()
    {
        connectionText.text = "Trying to connect";
        PhotonNetwork.ConnectUsingSettings(); // connects to Photon master servers
    }
}

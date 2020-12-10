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

    [Tooltip("The exit button game object")]
    [SerializeField]
    private GameObject exitButton;

    [Tooltip("Text to display the connection status")]
    [SerializeField]
    private TextMeshProUGUI connectionText;





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
    }

    public void ExitClick()
    {
        //connectionText.text = "Program Exit";
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

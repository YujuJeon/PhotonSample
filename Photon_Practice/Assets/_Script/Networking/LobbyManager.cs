using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";
    private GameObject defaultPage;
    private GameObject settingPage;

    public TextMeshProUGUI connectionInfoText;
    public TextMeshProUGUI playerText;
    public Button joinButton;
    public Button settingButton;
    public Button confirmButton;
    public Button observerButton;
    public GameObject myself;

    public bool isButtonPressed = false;

    // Start is called before the first frame update
    private void Start()
    {
        //photon cloud server
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        defaultPage = joinButton.transform.parent.gameObject;
        settingPage = confirmButton.transform.parent.gameObject;

        connectionInfoText.text = "Connecting To Master Server...";        
    }

    //public void LeaveLobby()
    //{
    //    PhotonNetwork.LoadLevel("SignIn");
    //}

    public void Setting()
    {
        defaultPage.SetActive(false);
        settingPage.SetActive(true);
    }

    public void Confirm()
    {
        defaultPage.SetActive(true);
        settingPage.SetActive(false);
    }

    public void Connect()
    {
        //try to join the room
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "Offline : Connection Disabled - Try reconnecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected to " + PhotonNetwork.ServerAddress;

        PhotonNetwork.LocalPlayer.NickName = playerText.text;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()} - Try Reconnecting...";

        //try to reconnect
        PhotonNetwork.ConnectUsingSettings();
    }

    //need to network this function
    public void JoinAsObserver()
    {
        isButtonPressed = true;

        if (isButtonPressed)
        {
            observerButton.interactable = false;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room, Creating new Room";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room.";
        //do not use SceneManager, client and server will enter seperate scene
        PhotonNetwork.LoadLevel("Main");
        DontDestroyOnLoad(myself);
    }
}

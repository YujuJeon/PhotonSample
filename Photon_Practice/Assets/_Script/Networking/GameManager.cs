using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    //singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private static GameManager instance;
    
    public Transform[] spawnPos;
    public GameObject playerPrefab;
    public GameObject cubePrefab;

    GameObject lobbymanager;
    public bool tfObserver;

    // Start is called before the first frame update
    private void Start()
    {
        //run separetly
        SpawnPlayer();

        //exist one interatable object
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnObject();
        }

        lobbymanager = GameObject.Find("LobbyManager");
        tfObserver = lobbymanager.GetComponent<LobbyManager>().isButtonPressed;        
    }

    private void SpawnPlayer()
    {
        var localPlaterIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        //block out of range error
        var sPos = spawnPos[localPlaterIndex % spawnPos.Length];

        PhotonNetwork.Instantiate(playerPrefab.name, sPos.position, sPos.rotation);
    }

    private void SpawnObject()
    {
        PhotonNetwork.Instantiate(cubePrefab.name, Vector3.zero, Quaternion.identity);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}

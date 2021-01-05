using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCube : MonoBehaviourPun
{
    MeshRenderer mesh;
    Material material;

    public bool IsMaterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        material = mesh.material;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            material.color = Color.yellow;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            material.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        if(!IsMaterClientLocal|| PhotonNetwork.PlayerList.Length < 2)
        {
            return;
        }
    }
}

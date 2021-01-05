using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody playerRigidbody;
    private MeshRenderer playerRenderer;

    public float speed = 3.0f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();

        if (photonView.IsMine)
        {
            playerRenderer.material.color = Color.red;
        }
        else
        {
            playerRenderer.material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //remote
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            
            playerRigidbody.transform.Rotate(0, x, 0);
            playerRigidbody.transform.Translate(0, 0, z);
        }            
    }
}

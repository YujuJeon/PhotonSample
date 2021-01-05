using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SamplePlayerController : MonoBehaviourPun
{
    private Rigidbody playerRigidbody;
    private MeshRenderer playerRenderer;

    public TextMeshProUGUI userText;
    public TextMeshProUGUI observerText;
    public PhotonView pv;

    public float speed = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm.tfObserver)
        {
            if (photonView.IsMine)
            {
                userText.text = PhotonNetwork.NickName;
                playerRenderer.material.color = Color.red;
                observerText.text = "Observer";
            }
            else
            {
                userText.text = pv.Owner.NickName;
                playerRenderer.material.color = Color.white;
            }
        }
        else
        {
            if (photonView.IsMine)
            {
                userText.text = PhotonNetwork.NickName;
                playerRenderer.material.color = Color.red;
            }
            else
            {
                userText.text = pv.Owner.NickName;
                playerRenderer.material.color = Color.white;
                observerText.text = "Observer";
            }
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

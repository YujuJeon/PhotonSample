using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviourPun
{
    //asking if you are host and gameobject is local gameobject
    //true = host false client
    public bool IsMasterClientLocal => PhotonNetwork.IsMasterClient && photonView.IsMine;

    private Vector3 direction = Vector3.right;
    private readonly float speed = 10.0f;
    private readonly float randomRefectionIntensity = 0.1f;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!IsMasterClientLocal || PhotonNetwork.PlayerList.Length < 2)
        {
            return;
        }

        var distance = speed * Time.deltaTime;
        //var hit = Physics.Raycast(transform.position, direction, distance);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                direction = Vector3.Reflect(direction, hit.normal);
                direction += Random.insideUnitSphere * randomRefectionIntensity;
            }

            transform.position = transform.position + direction * distance;
        }        
    }
}

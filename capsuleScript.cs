using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsuleScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        //offset = transform.position - player.transform.position;
        //offset = player.transform.position;
    }


    void Update()
    {
        if (player == null)
        {
            //Destroy(this.gameObject);
        }

        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + offset;
        }
    }
}
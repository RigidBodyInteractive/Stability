using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class FollowMe : MonoBehaviour
{

    public GameObject target;
    public Transform player;
    //private Vector3 offset;
    public float xPos;
    public float yPos;
    public float zPos;

    public float xScale;
    public float yScale;
    public float zScale;

    public float xRot;
    public float yRot;
    public float zRot;




    void awake()
    {
        //offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        transform.position = new Vector3(target.transform.position.x + xPos, target.transform.position.y + yPos, target.transform.position.z + zPos);

        //transform.rotation = Quaternion.Euler(target.transform.rotation.x + xRot, target.transform.rotation.y + yRot, target.transform.rotation.z + zRot);
        transform.LookAt(player);

        //transform.localScale = (player.transform.localScale);
    }


}
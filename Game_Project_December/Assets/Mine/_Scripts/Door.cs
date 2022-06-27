using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject player;
    string door;

    private void Start()
    {
        player = GameObject.Find("Player");
        door = gameObject.name;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage("DoorTrigger", door);
        }
    }
}

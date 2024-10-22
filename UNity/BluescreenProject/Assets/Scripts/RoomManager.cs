using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //If BL collides with a void, he will play an animation to move forward, screen will fade to black, and switch to the next camera in camera list
    [SerializeField] List<Camera> cams;

    internal void BlMoveDoor(Interactable door, GameObject bl)
    {
        //Animation cutscene thing that would move the player into the void and through the door, and use the camera on the other side
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

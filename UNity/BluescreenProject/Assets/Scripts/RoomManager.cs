using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //If BL collides with a void, he will play an animation to move forward, screen will fade to black, and switch to the next camera in camera list
    [SerializeField] List<Camera> cams;

    internal void BlMoveDoor(Doors door, GameObject bl, Doors newDoor)
    {
        //Animation cutscene thing that would move the player into the void and through the door, and use the camera on the other side
        
        var newDoorPos = newDoor.transform.position;
        var ogDoorPos = door.transform.position;

        //cut to black

        float x = 0;
        float z = 0;
        //check the travelling door's rotation and direction from the original door
        if(newDoor.transform.rotation.z == 90 || newDoor.transform.rotation.z == -90)
        {
            if(ogDoorPos.x > newDoorPos.x)
            {
                x = -1;
            }
            else
            {
                x = 1;
            }
        }
        else
        {
            if (ogDoorPos.z > newDoorPos.z)
            {
                z = -1;
            }
            else
            {
                z = 1;
            }
        }

        bl.transform.position = new Vector3(newDoorPos.x + x, 0, newDoorPos.z + z);
        //remove blindfold
        //AUTOSAVE
        newDoor.CloseDoor();
    }

    internal void ChangeCamera()
    {
        
    }
}

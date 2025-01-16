using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //screen will have to fade
    internal void BlMoveDoor(Doors door, GameObject bl, Doors newDoor)
    {
        //Animation cutscene thing that would move the player into the void and through the door, and use the camera on the other side
        
        var newDoorPos = newDoor.transform.position;
        var ogDoorPos = door.transform.position;

        //cut to black

        float x = 0;
        float z = 0;
        Debug.Log(newDoor.transform.rotation.z);

        //check the travelling door's rotation and direction from the original door
        if(newDoor.transform.rotation.z == -0.5 || newDoor.transform.rotation.z == 0.5)
        {
            if(ogDoorPos.x > newDoorPos.x)
            {
                x = -1.5f;
                z = 0;
            }
            else
            {
                x = 1.5f;
                z = 0;
            }
        }
        else
        {
            if (ogDoorPos.z > newDoorPos.z) //Some doors make BL stuck, but why? Maybe colliders?
            {
                x = 0;
                z = -1.5f;
            }
            else
            {
                x = 0;  
                z = 1.5f;
            }
        }

        bl.transform.position = new Vector3(newDoorPos.x + x, 0, newDoorPos.z + z);
        //Vector3 rot = bl.transform.rotation.eulerAngles;
        //Maybe rotation does not work because of the mouse feature....
        
        //bl.transform.Rotate(new Vector3(rot.x, rot.y + rotateBy, rot.z), Space.World);
        //Honestly im just going to make the doors be behind eachother because this rotation thing would be barely used

        //remove blindfold
        //AUTOSAVE
    }

    internal void ChangeCamera()
    {
        
    }
}

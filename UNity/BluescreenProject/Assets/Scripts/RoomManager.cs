using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Image blackscreen;
    Doors travelFrom;
    GameObject blue;
    Doors travelTo;

    internal void BlMoveDoor(Doors door, GameObject bl, Doors newDoor)
    {
        travelFrom = door;
        blue = bl;
        travelTo = newDoor;
        darkenScreen = true;
        changeScreenState = true;
    }

    private void ChangeBLPosition(Doors door, GameObject bl, Doors newDoor)
    {
        var newDoorPos = newDoor.transform.position;
        var ogDoorPos = door.transform.position;

        float x = 0;
        float z = 0;

        if (newDoor.transform.rotation.z == -0.5 || newDoor.transform.rotation.z == 0.5)
        {
            if (ogDoorPos.x > newDoorPos.x)
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
            if (ogDoorPos.z > newDoorPos.z)
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
        changeScreenState = true;
        darkenScreen = false;
    }

    float alpha = 0;
    Color newColor = Color.black;
    private bool changeScreenState = false;
    private bool? darkenScreen = false;

    private void Update()
    {
        //bool to check if blacksreen should start

        //bool to see if it should darken, stay, or clear up

        if (changeScreenState)
        {
            if (darkenScreen == true) //darken
            {
                alpha += Time.deltaTime * 3;

                if (alpha > 3)
                {
                    darkenScreen = null;
                    alpha = 3;
                }

                newColor.a = alpha;
            }
            else if (darkenScreen == false) //brighten
            {
                alpha -= Time.deltaTime * 3;

                if (alpha < 0)
                {
                    changeScreenState = false;
                    alpha = 0;
                }

                newColor.a = alpha;
            }
            else
            {
                changeScreenState = false;
                ChangeBLPosition(travelFrom, blue, travelTo);
                
            }
            blackscreen.color = newColor;
            Debug.Log(alpha);
        }
    }
}

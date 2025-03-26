using UnityEngine;

public class MainCamera : MonoBehaviour
{

    Portal[] portals;

    void Awake()
    {
        portals = FindObjectsByType<Portal>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
    }

    void OnPreCull()
    {

        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PrePortalRender();
        }
        for (int i = 0; i < portals.Length; i++)
        {
            //also moves the portal
            portals[i].Render();
        }

        for (int i = 0; i < portals.Length; i++)
        {//moves the portal
            portals[i].PostPortalRender();
        }

    }

}
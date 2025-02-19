using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCheck : MonoBehaviour
{
    public List<GameObject> gameObjectsInArea;
    public List<IntImageScale> highlightersInArea;
    private void OnTriggerEnter(Collider collision)
    {
        var go = collision.gameObject;
        if(go.GetComponent<Interactable>() || go.GetComponent<DiscoAnimations>() || go.GetComponent<Turret>() || go.GetComponent<Blob>())
        {
            gameObjectsInArea.Add(go);
        }
        if (go.GetComponentInChildren<IntImageScale>())
        {
            highlightersInArea.Add(go.GetComponentInChildren<IntImageScale>());
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        var go = collision.gameObject;

        if (gameObjectsInArea.Contains(go))
        {
            gameObjectsInArea.Remove(go);
            //Debug.Log("leave the fuck NOOOOW " + go);
        }
        if (go.GetComponentInChildren<IntImageScale>())
        {
            highlightersInArea.Remove(go.GetComponentInChildren<IntImageScale>());
            go.GetComponentInChildren<IntImageScale>().HlBool(false);
        }
    }

    public GameObject GetGameObject(int x)
    {
        return gameObjectsInArea[x].gameObject;
    }   

    public GameObject GetGameObject()
    {
        for(int i = 0; i < gameObjectsInArea.Count; i++)
        {
            GameObject go = gameObjectsInArea[i];
            return go;
        }
        return this.gameObject;
    }

    public List<GameObject> GetGameObjectList()
    {
        return gameObjectsInArea;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForwardCheck : MonoBehaviour
{
    public List<GameObject> gameObjectsInArea;


    private void OnTriggerEnter(Collider collision)
    {
        var go = collision.gameObject;
        if (!go.GetComponent<Interactable>()) return;
        if (go.activeSelf)
        gameObjectsInArea.Add(go);
    }
    private void OnTriggerExit(Collider collision)
    {
        var go = collision.gameObject;

        if (gameObjectsInArea.Contains(go))
        {
            gameObjectsInArea.Remove(go);
            //Debug.Log("leave the fuck NOOOOW " + go);
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

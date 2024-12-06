using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    /*TODO:
     * model Roof --
     * place Cameras and make triggers for them
     * Interacting with items --
     *      -clipboard 
     *      -keys --
     *      -doors -
     * Make the Main Menu screen a little more recognizable
     */
    [SerializeField] GameObject bl;
    [SerializeField] List<GameObject> canvas;
    ForwardCheck fc;
    RoomManager rm;

    public List<Items> pickedUpItems;


    // Start is called before the first frame update
    void Start()
    {
        fc = bl.GetComponentInChildren<ForwardCheck>();
        rm = GetComponent<RoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        EscapeKey();
        InteractKey();
    }

    private void InteractKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int? closestIndex = FindClosestItem();
            if (closestIndex == null) return;
            Interactable interactItem = fc.gameObjectsInArea[(int)closestIndex].GetComponent<Interactable>();
            Debug.Log(interactItem);
            interactItem.Interact();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HideLore();
        }
    }

    private int? FindClosestItem()
    {
        List<GameObject> list = fc.gameObjectsInArea;
        int? r = null;
        double distance = 99999;
        for(int i = 0; i < list.Count; i++)
        {
            GameObject go = list[i];
            double x = bl.transform.position.x - go.transform.position.x;
            double z = bl.transform.position.z - go.transform.position.z;

            double distance2 = Math.Sqrt(x * x + z * z);
            if(distance2 < distance)
            {
                r = i;
                distance = Math.Sqrt(x*x + z*z);
            }
        }
        return r;
    }

    private static void EscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Interaction(Items item)
    {
        pickedUpItems.Add(item);
    }
    public List<Items> GetPickedItems()
    {
        return pickedUpItems;
    }
    public ForwardCheck ForwCheck()
    {
        return fc;
    }

    internal void BLTransition(Doors door, Doors travelTo)
    {
        Debug.Log($"{door},  {travelTo},   {bl}");
        rm.BlMoveDoor(door, bl, travelTo);
    }

    int nomberForDaLore = 80;
    void HideLore()
    {
        int n = nomberForDaLore;
        bl.GetComponent<BLPlayerMovement>().StopMoving(false);
        var ts = canvas[n].GetComponentsInChildren<TextMeshProUGUI>();
        canvas[n].SetActive(false);
        ts[0].text = "";
        ts[1].text = "";
    }
    internal void ShowLore(string text, string info, int n)
    {
        nomberForDaLore = n;
        var ts = canvas[n].GetComponentsInChildren<TextMeshProUGUI>();
        canvas[n].SetActive(true);
        ts[0].text = text;
        ts[1].text = info;
        bl.GetComponent<BLPlayerMovement>().StopMoving(true);
    }
}

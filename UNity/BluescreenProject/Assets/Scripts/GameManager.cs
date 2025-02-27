using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] BLPlayerMovement bl;
    ForwardCheck fc;
    RoomManager rm;

    public List<Items> pickedUpItems;
    public List<Interactable> interacted;
    public void LoadData(GameData data)
    {
        this.bl.transform.position = data.playerPos;
        this.pickedUpItems = data.pickedUpItems;
        this.interacted = data.interacted;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = this.bl.transform.position;
        data.pickedUpItems = this.pickedUpItems;
        data.interacted = this.interacted;
    }

    public void ChangeBLMovement()
    {
        if (bl.IsMoving())
        {
            bl.StopMoving(false);
        }
        else
        {
            bl.StopMoving(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        fc = bl.gameObject.GetComponentInChildren<ForwardCheck>();
        rm = GetComponent<RoomManager>();
        foreach (Items item in pickedUpItems)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void AddToInteractedList(Interactable inter)
    {
        interacted.Add(inter);
    }
    bool firstLoad = true;
    // Update is called once per frame
    void Update()
    {
        FirstLoad();
        EscapeKey();
        InteractKey();
        int? pok = FindClosestImageScaler();
        if (pok != null)
        {
            fc.highlightersInArea.ForEach(i => { i.HlBool(false); });
            fc.highlightersInArea[(int)pok].HlBool(true);
        }
    }

    private void FirstLoad()
    {
        if (firstLoad)
        {
            firstLoad = false;
            foreach (Interactable interactable in interacted)
            {
                interactable.ForceInteract();
            }
        }
    }

    private void InteractKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int? closestIndex = FindClosestItem();
            if (closestIndex == null) return;
            var g = fc.gameObjectsInArea[(int)closestIndex];

                if(g.GetComponent<DiscoAnimations>() != null)
                {
                    var en = g.GetComponent<DiscoAnimations>();
                    en.DamageMe();
                }
                else if (g.GetComponent<Turret>() != null)
                {
                    var en = g.GetComponent<Turret>();
                    en.DamageMe();
                }
                else if (g.GetComponent<Blob>() != null)
                {
                    var en = g.GetComponent<Blob>();
                    en.DamageMe();
                }
            
            else if(g.GetComponent<Interactable>() != null)
            {
                Interactable interactItem = g.GetComponent<Interactable>();
                interactItem.Interact();
            }

            
            //Debug.Log(interactItem);
        }
    }
    private int? FindClosestItem() //use vectors to measure distance
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
    private int? FindClosestImageScaler()
    {
        List<IntImageScale> list = fc.highlightersInArea;
        int? r = null;
        float far = 9999;
        for (int i = 0; i < list.Count; i++)
        {
            var distance = Vector3.Distance(list[i].transform.position, bl.transform.position);

            if(distance < far)
            {
                far = distance;
                r = i;
            }
        }
        return r;
    }
    private static void EscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        rm.BlMoveDoor(door, bl.gameObject, travelTo);
    }
    public Vector3 BLPos()
    {
        return bl.transform.position;
    }
}

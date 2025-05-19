using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] BLPlayerMovement bl;
    [SerializeField] GameObject escape;
    ForwardCheck fc;
    RoomManager rm;

    public List<Items> pickedUpItems;
    public List<Interactable> interacted;
    public void LoadData(GameData data)
    {
        this.bl.transform.position = data.playerPos;
        //this.interacted = data.interacted;

        List<string> listOfGuids = new List<string>();
        List<Interactable> intsToAdd = new List<Interactable>();
        foreach(KeyValuePair<string, bool> pair in data.interactedWith)
        {
            if (pair.Value)
            {
                listOfGuids.Add(pair.Key);
            }
        }
        foreach (var item in GetComponentsInChildren<Interactable>())
        {
            for(int i = 0; i < listOfGuids.Count; i++)
            {
                if(item.id == listOfGuids[i])
                {
                    listOfGuids.RemoveAt(i);
                    intsToAdd.Add(item);
                    break;
                }
            }
        }
        this.interacted = intsToAdd;
    }

    public void SaveData(ref GameData data)
    {
        if(bl != null)
        {
            data.playerPos = this.bl.transform.position;
            data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            //data.interacted = this.interacted;
            foreach(Interactable item in this.interacted)
            {
                if (data.interactedWith.ContainsKey(item.id))
                {
                    data.interactedWith.Remove(item.id);
                }
                data.interactedWith.Add(item.id, item.interactedWith);
            }
        }
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
        inter.interactedWith = true;
        interacted.Add(inter);
    }
    bool firstLoad = true;
    void Update()
    {
        if (firstLoad)
        {
            FirstLoad();
            firstLoad = false;
        }

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
        foreach (Interactable interactable in interacted)
        {
            Debug.Log(interactable);
            interactable.ForceInteract();
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
                g.GetComponent<Interactable>().Interact();
            }
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
    private void EscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!escape.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                escape.SetActive(true);
                ChangeBLMovement();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                escape.SetActive(false);
                ChangeBLMovement();
            }
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
    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

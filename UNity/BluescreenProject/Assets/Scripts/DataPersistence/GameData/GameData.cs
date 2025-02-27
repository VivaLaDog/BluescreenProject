using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //I want to save: BL's position, held items, done puzzles and dead enemies.
    public Vector3 playerPos;
    public List<Items> pickedUpItems;
    public List<Interactable> interacted;
    public GameData()
    {
        this.playerPos = new Vector3(13f, 0, 33.5f);
        this.pickedUpItems = new List<Items>();
        this.interacted = new List<Interactable>();
    }

}

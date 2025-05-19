using UnityEngine;

[System.Serializable]
public class GameData
{
    //I want to save: BL's position, held items, done puzzles and dead enemies.
    public Vector3 playerPos;
    public SerializableDictionary<string, bool> interactedWith;
    public int sceneIndex;
    public GameData()
    {
        this.playerPos = new Vector3(13f, 0, 33.5f);
        this.interactedWith = new SerializableDictionary<string, bool>();
        this.sceneIndex = 1;
    }

}

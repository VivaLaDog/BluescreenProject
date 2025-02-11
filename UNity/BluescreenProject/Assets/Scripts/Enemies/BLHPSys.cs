using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BLHPSys : MonoBehaviour
{
    [SerializeField] List<Sprite> bloodPrefabs;
    [SerializeField] Transform bloodOverlay;
    [SerializeField] RectTransform canvas;
    /* Idea:
             * Use two lists, active and connected blood splatters
             * whenever we turn on a blood splatter, we create a new one on the canvas with random position, rotation and scale
             * after 10 seconds (healed) we remove the oldest one (position 0 in the list)
             * 
             * the longer the splatter is alive, the more transparent it is
             */
    List<Sprite> bloodOverlays = new List<Sprite>();
    private int health;
    private int maxHealth = 10;

    float healTime = 10000;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealCheck();
    }


    private void HealCheck() //basically healing
    {
        if (health > 0 && health < maxHealth)//if i'm not dead, hurt and not bleeding
        {
            healTime -= Time.deltaTime;
            if (healTime <= 0) //heal 1hp every 10 seconds
            {
                health++;
                healTime = 10f;
            }
        }

    }
    
    public void Damage(int amount)
    {
        Debug.Log("Viva is hit"); // on hit make a camera shake
        
        if(health - amount <= 0)
        {
            Debug.Log("Viva is kill");
            /* a ramp up of:
             * Camera shake, chromatic abberation gets stronger (maybe with making lights stronger)
             * effects get super strong and then loads the current scene again
             * 
             * requires the animation to play
             */
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else //show a damage overlay
        {
            

            //creates a random blood prefab

            int a = Random.Range(0, bloodPrefabs.Count);


            var x1 = canvas.GetComponent<Canvas>().scaleFactor * canvas.rect.width;
            var y1 = canvas.GetComponent<Canvas>().scaleFactor * canvas.rect.height;

            var x = Random.Range(-(x1/2), x1/2);
            var y = Random.Range(-(y1/2), y1/2);
            var Rz = Random.Range(0, 360);

            var c = Instantiate(bloodPrefabs[a],
                    new Vector3(x, y, 0),
                    Quaternion.Euler(0, 0, Rz), bloodOverlay);
            
            bloodOverlays.Add(c);
            AddOverlay.Invoke();
        }
        health -= amount;
    }
    private Action AddOverlay => OnAddOverlay;
    private void OnAddOverlay()
    {
        Instantiate(bloodOverlays[bloodOverlays.Count - 1]);
    }
}

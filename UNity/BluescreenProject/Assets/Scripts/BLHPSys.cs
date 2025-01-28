using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BLHPSys : MonoBehaviour
{
    [SerializeField] List<Sprite> bloodPrefabs;
    [SerializeField] Transform bloodOverlay;
    [SerializeField] Camera mainCamera;
    
    List<Sprite> bloodOverlays;
    private int health;
    private int maxHealth = 10;

    float healTime = 10000;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    async void Update()
    {
        if(bleeding) // look into asyncs, to allow a while
        {
          await Bleeding();
        }
        HealCheck();
    }

    async Task Bleeding()
    {
        var timePassed = bleedTime / bleedAmount;
        while(bleedTime > 0)
        {
            bleedTime -= Time.deltaTime;
            timePassed -= Time.deltaTime;
            if (timePassed <= 0)
            {
                Damage(1);
                timePassed = bleedTime / bleedAmount;
            }
        }
        bleeding = false;

        return;
    }

    private void HealCheck() //basically healing
    {
        if (health > 0 && health < maxHealth && !bleeding)//if i'm not dead, hurt and not bleeding
        {
            healTime -= Time.deltaTime;
            if (healTime <= 0) //heal 1hp every 10 seconds
            {
                health++;
                healTime = 10000;
            }
        }

    }
    
    public void Damage(int amount)
    {
        Debug.Log("Viva is hit");
        
        if(health - amount <= 0)
        {
            Debug.Log("Viva is kill");
            /* a ramp up of:
             * Camera shake, chromatic abberation gets stronger (maybe with making lights stronger)
             * effects get super strong and then loads the current scene again
             */
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else //show a damage overlay
        {
            /* Idea:
             * Use two lists, active and connected blood splatters
             * whenever we turn on a blood splatter, we create a new one on the canvas with random position, rotation and scale
             * after 10 seconds (healed) we remove the oldest one (position 0 in the list)
             * 
             * the longer the splatter is alive, the more transparent it is
             */
            //creates a random blood prefab
            var x = Random.Range(0, mainCamera.pixelWidth);
            var y = Random.Range(0, mainCamera.pixelHeight);
            var Rz = Random.Range(0, 360);
            var c = Instantiate(bloodPrefabs[Random.Range(0, bloodOverlays.Count + 1)], 
                    new Vector3(x, y, 0),
                    Quaternion.Euler(0, 0, Rz), bloodOverlay);
            bloodOverlays.Add(c);
            
        }
        health -= amount;
    }
    float bleedTime;
    int bleedAmount;
    bool bleeding = false;
    public void Bleed(int totalAmount, float timeMS)
    {
        bleedAmount = totalAmount;
        bleedTime = timeMS;
        bleeding = true;
    }
}

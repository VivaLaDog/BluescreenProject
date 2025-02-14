using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BLHPSys : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Camera blCam;
            /* Idea:
             * the longer the splatter is alive, the more transparent it is
             */

    private int health;
    private int maxHealth = 10;
    bool blIsKill = false;
    float healTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    bool chicanery = true;
    float timeToDeath = 5f;
    // Update is called once per frame
    void Update()
    {
        if (blIsKill)
        {
            if (chicanery)
            {
                Animator cameraAnimator = blCam.GetComponent<Animator>();
                cameraAnimator.SetBool("Dead", true);
                //play camera animation
                chicanery = false;
            }
            else
            {
                timeToDeath -= Time.deltaTime;
                if(timeToDeath <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
        HealCheck();
    }


    private void HealCheck() //basically healing
    {
        if (health > 0 && health < maxHealth)//if i'm not dead, am hurt
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
        healTime = 10f;

        if (health - amount <= 0)
        {
            Debug.Log("Viva is kill");
            blIsKill = true;


            /* a ramp up of:
             * Camera shake, chromatic abberation gets stronger (maybe with making lights stronger)
             * effects get super strong and then loads the current scene again
             * 
             * requires the animation to play
             */
        }
        else //show a damage overlay
        {
            //the lower the health, the more chromatic abberation + desaturation

            AddNewBloodSprite();
        }
        health -= amount;
    }

    private void AddNewBloodSprite()
    {

        AddOverlay?.Invoke();
    }

    private Action AddOverlay => OnAddOverlay;
    private void OnAddOverlay()
    {

    }
}

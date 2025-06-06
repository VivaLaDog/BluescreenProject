using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BLHPSys : MonoBehaviour
{
    [SerializeField] List<Image> bloodImages;
    [SerializeField] Camera blCam;
            /* Idea:
             * the longer the splatter is alive, the more transparent it is
             */

    private int health;
    private int maxHealth = 5;
    bool blIsKill = false;
    [SerializeField]
    AudioSource deathSfx;
    void Start()
    {
        health = maxHealth;
        Animator cameraAnimator = blCam.GetComponent<Animator>();
        cameraAnimator.SetBool("New", true);
    }
    bool chicanery = true;
    float timeToDeath = 2f;
    // Update is called once per frame
    void Update()
    {
        if (blIsKill)
        {
            if (chicanery)
            {
                Animator cameraAnimator = blCam.GetComponent<Animator>();
                cameraAnimator.SetBool("Dead", true);
                deathSfx.Play();
                chicanery = false;
            }
            else
            {
                timeToDeath -= Time.deltaTime;
                if(timeToDeath <= 0)
                {
                    Animator cameraAnimator = blCam.GetComponent<Animator>();
                    cameraAnimator.SetBool("Dead", false);
                    blIsKill = false;
                    timeToDeath = 2f;
                    chicanery = true;
                    DataPersistenceManager.Instance.LoadGame();
                }
            }
        }
        else
        {
            HealCheck();
            if(removeBlood)
            OverlayRemover();
        }
    }
    bool removeBlood = false;
    private void OverlayRemover()
    {//slowly hides the overlays
        for (int i = 0; i < bloodImages.Count; i++)
        {
                Color newColour = bloodImages[i].color;
                newColour.a -= Time.deltaTime / 4;
                if (newColour.a <= 0)
                {
                    newColour.a = 0;
                }
                bloodImages[i].color = newColour;
            
        }
    }

    float healTime = 2f;
    private void HealCheck() //basically healing
    {
        if (health > 0 && health < maxHealth)//if i'm not dead, am hurt
        {
            removeBlood = true;
            healTime -= Time.deltaTime;
            if (healTime <= 0) //heal 1hp every 2 seconds
            {
                health++;
                healTime = 2f;
            }
        }

    }
    
    public void Damage(int amount)
    { // on hit make a camera shake
        healTime = 2f;
        if (health - amount <= 0)
        {
            blIsKill = true;
        }
        else //show a damage overlay
        {
            //the lower the health, the more chromatic abberation + desaturation?
            int a = Mathf.Abs((health-amount)/2 - 1);
            Color change = bloodImages[a].color;
                change.a = 1;
            bloodImages[a].color = change;
            health -= amount;
        }
    }
}

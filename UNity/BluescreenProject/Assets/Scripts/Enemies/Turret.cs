using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance;
    [SerializeField] float attackDistance;

    int health = 999;
    float time = 3f;

    protected override void Damage()
    {
        health--;
        if(health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        GetComponent<Animator>().SetBool("Off", true);
    }

    internal void DamageMe()
    {
        Damage();
    }
    bool firstTimeForSound = true;
    // Update is called once per frame
    void Update()
    {
        var c = Vector3.Distance(transform.position, target.position);

        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            transform.LookAt(target);
            //Debug.Log(transform.name + " sees you!");

            if (c < attackDistance)
            {
                if (firstTimeForSound)
                {
                    firstTimeForSound = false;
                    //play sound of locking on
                }
                //BEGIN the ATTACK!
                //count to 3, shoot (take health from player) repeat
                time -= Time.deltaTime;
                if(time <= 0)
                {
                    //sound of shooting
                    time = 3f;
                    firstTimeForSound = true;
                    target.GetComponentInParent<BLHPSys>().Damage(4);
                }
            }
        }
    }
}

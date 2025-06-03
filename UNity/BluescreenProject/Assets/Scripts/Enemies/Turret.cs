using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance;
    [SerializeField] float attackDistance;
    [SerializeField] AudioSource warmupSfx;
    [SerializeField] AudioSource fireSfx;

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
    // Update is called once per frame
    void Update()
    {
        var c = Vector3.Distance(transform.position, target.position);

        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            transform.LookAt(target);
            warmupSfx.Play();

            if (c < attackDistance)
            {
                //BEGIN the ATTACK!
                //count to 3, shoot (take health from player) repeat
                time -= Time.deltaTime;
                if(time <= 0)
                {
                    fireSfx.Play();
                    time = 3f;
                    target.GetComponentInParent<BLHPSys>().Damage(4);
                }
            }
        }
        else
        {
            if (warmupSfx.isPlaying)
                warmupSfx.Stop();
            if(time < 3f)
            time = 3f;
        }
    }
}
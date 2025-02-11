using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Blob : Enemy
{
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance;
    [SerializeField] float attackDistance;
    int health = 10;
    float x = 0f;

    Vector3 ogPos;

    Animator animator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (health > 0)
            DistanceCheck();
    }
    float timer = 0f;
    private void DistanceCheck()
    {
        //checks distance between og position and player, because i want this fella to only guard an area
        var a = Vector3.Distance(ogPos, target.position);
        var b = Vector3.Distance(transform.position, target.position);
        
        //checks distance between player and blob, if within viewdistance and not obscured
        //if the player is within attack distance, it will attack
        
        if (a < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            agent.SetDestination(target.position);

            if (b < attackDistance)
            {
                if(timer <= 0)
                {
                    agent.isStopped = true;
                    x += Time.deltaTime * speed * 2;
                    //BEGIN the ATTACK!
                    target.GetComponentInParent<BLHPSys>().Damage(2);
                    timer = 1f;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                x -= Time.deltaTime * speed * 5;
                agent.isStopped = false;
            }
        }
        else
        {
            agent.SetDestination(ogPos);
        }
        if (x < 0f) x = 0;
        if (x > 1f) x = 1f;

        animator.SetFloat("IsAttacking", x);


    }

    protected override void Die()
    {
        agent.enabled = false;
        ogPos = transform.position;
    }
    public void DamageMe()
    {
        Damage();
    }
    protected override void Damage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }
}

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Blob : Enemy
{
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance;
    [SerializeField] float attackDistance;
    [SerializeField] AudioSource attackSfx;
    int health = 10;
    bool x = false;

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
                    x = true;
                    attackSfx.Play();
                    target.GetComponentInParent<BLHPSys>().Damage(2);
                    timer = 1f;
                }
                timer -= Time.deltaTime;
            }
            else
            {
                if(timer < 1)
                    timer = 1f;
                x = false;
                agent.isStopped = false;
            }
        }
        else
        {
            x = false;
            agent.SetDestination(ogPos);
        }
        animator.SetBool("IsAttacking", x);
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

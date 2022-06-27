using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 8.0f;
    bool walkBool = true;
    bool attackBool = true;

    Transform target;
    CharacterStats targetStats;
    NavMeshAgent agent;
    EnemyCombat combat;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        targetStats = target.GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<EnemyCombat>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (walkBool)
            {

                StartCoroutine(WalkAnim());
            }

            if (distance <= agent.stoppingDistance)
            {
                lookTarget();
                if (attackBool)
                {
                    StartCoroutine(AttackAnim());
                }
                combat.AttackPlayer(targetStats);
            }
        }
    }

    void lookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        if (gameObject.tag == "Boss3")
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    IEnumerator WalkAnim()
    {
        walkBool = false;

        animator.SetTrigger("WalkAnim");

        yield return new WaitForSeconds(2);

        walkBool = true;


    }
    IEnumerator AttackAnim()
    {
        attackBool = false;

        animator.SetTrigger("AttackAnim");

        yield return new WaitForSeconds(1);

        attackBool = true;


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform eTransform;

    Player player;

    // AI agent for this Enemy.
    // This agent is responsible for finding paths to player for the enemy.
    NavMeshAgent agent;

    Animator animator;

    float movSpeed = 9.0f;

    float rotSpeed = 30;

    int health = 15;

    float timer = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        eTransform = this.transform;
        animator = this.GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = movSpeed;

        agent.SetDestination(player.getPTranform().position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getHealth() <= 0) return;
        AnimatorStateInfo cur = animator.GetCurrentAnimatorStateInfo(0);

        // Based on current animation state, decide whether we should enter the
        // different state.
        if (cur.shortNameHash == Animator.StringToHash("idle") &&
            !animator.IsInTransition(0))
        {
            animator.SetBool("idle", false);
            timer -= Time.deltaTime;
            if (timer > 0) return;

            if (Vector3.Distance(eTransform.position, player.getPTranform().position) <= 1.2f)
            {
                animator.SetBool("attack", true);
            } else
            {
                // Reset the timer
                timer = 1.5f;
                agent.SetDestination(player.getPTranform().position);
                animator.SetBool("run", true);
            }
        } else if (cur.shortNameHash == Animator.StringToHash("run") &&
            !animator.IsInTransition(0))
        {
            animator.SetBool("run", false);

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                agent.SetDestination(player.getPTranform().position);
                timer = 1.5f;
            }

            if (Vector3.Distance(eTransform.position, player.getPTranform().position) <= 1.2f)
            {
                animator.SetBool("attack", true);
            }
        } else if (cur.shortNameHash == Animator.StringToHash("attack") &&
            !animator.IsInTransition(0))
        {
            rotateToPlayer();
            animator.SetBool("attack", false);

            if (cur.normalizedTime >= 1.0f)
            {
                // if the animation for the current state is 100% played
                animator.SetBool("idle", true);
                timer = 1.0f;
            }
        }


    }

    void rotateToPlayer()
    {
        Vector3 dir = player.getPTranform().position - eTransform.position;
        Vector3 rotVector = Vector3.RotateTowards(eTransform.forward, dir,
            rotSpeed * Time.deltaTime, 0.0f);
        eTransform.rotation = Quaternion.LookRotation(rotVector);

    }
}


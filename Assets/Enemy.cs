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

    float movSpeed = 9.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        eTransform = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = movSpeed;

        agent.SetDestination(player.getPTranform().position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


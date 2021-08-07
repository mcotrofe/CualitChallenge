using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIBrain : MonoBehaviour
{
    static readonly string ForwardParameter = "Forward";
    static readonly string RotateParameter = "Rotate";
    static readonly string RunParameter = "Run";

    public Transform Player;

    private NavMeshAgent agent;
    private Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = Player.position;
        
        animator.SetBool(RunParameter, agent.velocity.magnitude / agent.speed > .15f);
        animator.SetFloat(ForwardParameter, agent.velocity.magnitude / agent.speed);
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;
using CualitChallenge.Characters.Damage;

namespace CualitChallenge.Characters.AI
{

    [RequireComponent(typeof(CharacterCombat), typeof(NavMeshAgent), typeof(Animator))]
    public class AIBrain : MonoBehaviour
    {
        static readonly string ForwardParameter = "Forward";
        static readonly string RunParameter = "Run";
        static readonly string AimParameter = "Aim";

        [SerializeField] float stopRunningDistance = 10;
        [SerializeField] float attackDistance = 2;
        [SerializeField] float runSpeed = 6;
        [SerializeField] float combatSpeed = 3;
        [SerializeField] Vector2 attackDelayRandom = new Vector2(.5f, 5f);

        private NavMeshAgent agent;
        private Animator animator;
        private CharacterCombat characterCombat;
        private CharacterMainHealth characterHealth;
        private Transform target;
        private float attackDelay;
        private float attackTimer;

        public void SetTarget(Transform target) => this.target = target;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            characterCombat = GetComponent<CharacterCombat>();
            characterHealth = GetComponent<CharacterMainHealth>();
            characterHealth.OnHit.AddListener(OnHitCallback);
            attackDelay = UnityEngine.Random.Range(attackDelayRandom.x, attackDelayRandom.y);
        }

        void Update()
        {
            if (target == null) return;
            agent.destination = target.position;

            if (IsInCombatDistance())
            {
                UpdateCombatMovement();
                UpdateAttack();
            }
            else
            {
                UpdateRunMovement();
            }
        }

        private void UpdateAttack()
        {
            if (IsInAttackDistance())
            {
                attackTimer += Time.deltaTime;
                if(attackTimer > attackDelay)
                {
                    characterCombat.Attack();
                    attackDelay = UnityEngine.Random.Range(attackDelayRandom.x, attackDelayRandom.y);
                }
            }
        }


        private bool IsInCombatDistance() => DistanceToTarget() < stopRunningDistance;
        private bool IsInAttackDistance() => DistanceToTarget() < attackDistance;

        private void UpdateRunMovement()
        {
            agent.speed = Mathf.Lerp(agent.speed, runSpeed, Time.deltaTime * 5);
            animator.SetBool(AimParameter, false);
            animator.SetBool(RunParameter, agent.velocity.magnitude / agent.speed > .15f);
            animator.SetFloat(ForwardParameter, agent.velocity.magnitude / agent.speed);
        }

        private void UpdateCombatMovement()
        {
            agent.speed = Mathf.Lerp(agent.speed, combatSpeed, Time.deltaTime * 5);
            animator.SetBool(AimParameter, true);
            animator.SetBool(RunParameter, false);
            animator.SetFloat(ForwardParameter, agent.velocity.magnitude / agent.speed);
        }

        private float DistanceToTarget() => Vector3.Distance(transform.position, target.position);

        private void OnEnable() => agent.enabled = true;

        private void OnDisable() => agent.enabled = false;

        private void OnHitCallback() => attackTimer = 0;
    }
}
//Made by: Mathias Sorin
//Last updated: 18/06/2021

using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using MLAPI;
using MLAPI.Messaging;

public class EnemyAI : NetworkBehaviour
{
    [Header("AI Variables")]
    public float detectionRate = 0.5f;
    public float detectionRange = 10f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float attackRange = 5f;

    //Components
    private EnemyMain enemyMain;
    private EnemyAnimation enemyAnimation;
    private NavMeshAgent agent;

    //Check if coroutine is running
    private bool coroutineDetection;

    //State machine
    public enum State
    {
        Idle, Chase, Attack
    }

    //Current state
    public State currentState;

    private void Awake()
    {
        enemyMain = GetComponent<EnemyMain>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        agent.stoppingDistance = attackRange;
    }

    void Start()
    {
        Idle();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SimpleEnemyBehaviorServerRpc()
    {
        if (enemyMain.target)
        {
            if (GameManager.Instance.abilityCatalog.RangeCheck(enemyMain.target.transform.position, transform.position, attackRange))
            {
                Attack();
            }
            else
            {
                Chase();
            }
        }
        else
        {
            Idle();
        }
        enemyAnimation.Animate();
    }

    private void Idle()
    {
        currentState = State.Idle;
        if (coroutineDetection)
        {
            return;
        }
        CoroutineDetectPlayer(detectionRate);
    }

    private void Chase()
    {
        currentState = State.Chase;
        agent.SetDestination(enemyMain.target.transform.position);
    }

    private void Attack()
    {
        currentState = State.Attack;
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector3 direction = (enemyMain.target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void DetectPlayers(float detectionRange)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                NetworkObject networkObject = collider.GetComponent<NetworkObject>();
                enemyMain.Alert(networkObject);
                enemyMain.target = networkObject;
                return;
            }
        }
    }

    protected void CoroutineDetectPlayer(float time)
    {
        coroutineDetection = true;
        StartCoroutine(WaitForCooldown(time));
    }

    protected IEnumerator WaitForCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        coroutineDetection = false;
        DetectPlayers(detectionRange);
    }
}

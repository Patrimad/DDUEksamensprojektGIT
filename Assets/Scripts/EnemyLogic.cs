using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    private Transform player;
    public float speed;
    private NavMeshAgent agent;
    public int damage;

    public float chaseRange = 15f;
    public float attackRange = 2f;
    public float patrolRange;
    public float changeStateDelay = 3f;

    public Transform[] posts;

    private Vector3 lastKnownPlayerPosition;
    private bool hasLastKnownPosition = false;

    private float stateTimer = 0f;
    private bool investigatingLastPosition = false;

    private int currentPostIndex = 0;
    private float idleWaitTimer = 0f;
    public float idleWaitDuration = 2f;

    public AIState currentState = AIState.idle;

    private HealthSystem hs;

    public float attackCooldown = 1f;
    private float attackCooldownTimer = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) { player = playerObj.transform; }

        hs = player.gameObject.GetComponent<HealthSystem>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void FixedUpdate()
    {
        if (attackCooldownTimer > 0f)                  
            attackCooldownTimer -= Time.fixedDeltaTime;

        switch (currentState)
        {
            case AIState.idle:
                HandleIdle();
                break;
            case AIState.patrol:
                HandlePatrol();
                break;
            case AIState.chase:
                HandleChase();
                break;
            case AIState.attack:
                HandleAttack();
                break;
        }
    }

    void HandleIdle()
    {
        agent.isStopped = true;

        if (CanSeePlayer())
        {
            ChangeState(AIState.chase);
            return;
        }

        if (posts != null && posts.Length > 0)
        {
            idleWaitTimer += Time.fixedDeltaTime;
            if (idleWaitTimer >= idleWaitDuration)
            {
                idleWaitTimer = 0f;
                PatrolToNextPost();
            }
        }
    }

    void HandlePatrol()
    {
        agent.isStopped = false;

        if (CanSeePlayer())
        {
            ChangeState(AIState.chase);
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            ChangeState(AIState.idle);
        }
    }

    void HandleChase()
    {
        agent.isStopped = false;

        if (CanSeePlayer())
        {
            lastKnownPlayerPosition = player.position;
            hasLastKnownPosition = true;
            investigatingLastPosition = false;
            stateTimer = 0f;

            float distToPlayer = Vector3.Distance(transform.position, player.position);

            if (distToPlayer <= attackRange)
            {
                ChangeState(AIState.attack);
                return;
            }

            agent.SetDestination(player.position);
        }
        else
        {
            if (hasLastKnownPosition && !investigatingLastPosition)
            {
                agent.SetDestination(lastKnownPlayerPosition);
                investigatingLastPosition = true;
                stateTimer = 0f;
            }

            bool reachedLastKnown = investigatingLastPosition
                && !agent.pathPending
                && agent.remainingDistance < 0.5f;

            stateTimer += Time.fixedDeltaTime;
            bool timedOut = stateTimer >= changeStateDelay;

            if (reachedLastKnown || timedOut)
            {
                hasLastKnownPosition = false;
                investigatingLastPosition = false;
                stateTimer = 0f;
                ReturnToClosestPost();
            }
        }
    }

    void HandleAttack()
    {
        agent.isStopped = true;

        if (!CanSeePlayer())
        {
            ChangeState(AIState.chase);
            return;
        }

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 dir = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        if (distToPlayer > attackRange)
        {
            ChangeState(AIState.chase);
        }
        else if (attackCooldownTimer <= 0f)
        {
            hs.TakeDamage(damage);
            attackCooldownTimer = attackCooldown;
        }
    }

    void PatrolToNextPost()
    {
        if (posts == null || posts.Length == 0) return;

        currentPostIndex = (currentPostIndex + 1) % posts.Length;
        agent.isStopped = false;
        agent.SetDestination(posts[currentPostIndex].position);
        ChangeState(AIState.patrol);
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (distToPlayer > chaseRange) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dirToPlayer, out RaycastHit hit, chaseRange))
        {
            if (hit.transform == player)
                return true;
        }

        return false;
    }

    void ReturnToClosestPost()
    {
        if (posts == null || posts.Length == 0)
        {
            ChangeState(AIState.idle);
            return;
        }

        Transform closest = null;
        float closestDist = Mathf.Infinity;

        foreach (Transform post in posts)
        {
            float dist = Vector3.Distance(transform.position, post.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = post;
            }
        }

        if (closest != null)
        {
            agent.isStopped = false;
            agent.SetDestination(closest.position);
            ChangeState(AIState.patrol);
        }
    }

    void ChangeState(AIState newState)
    {
        currentState = newState;
        stateTimer = 0f;
    }
    public enum AIState { idle, patrol, chase, attack }
}
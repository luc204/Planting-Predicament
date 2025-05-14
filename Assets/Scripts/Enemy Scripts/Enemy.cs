using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum EnemyState { Idle, Chase, Attack, Dead }
public enum EnemyType { Melee, Ranged, Explosive }
public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public EnemyState enemyState;

    public GameObject target; //Player. 
    float distanceToTarget;

    public float moveSpeed = 3;
    public float rotateSpeed = 5;

    Animator anim;

    public float stoppingDistance = 1;

    [Range(10, 360)]
    public float viewAngle = 50;
    public float viewDistance = 10;

    bool canAttack;
    public float attackCooldown = 1;
    float attackCooldownTimer;

    //See you
    //Chase you
    //Attack you

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = DistanceToTarget();
        HandleState();

        //Logic if health is 0, die.
    }

    void HandleState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:

                Idle();

                break;
            case EnemyState.Chase:

                Chase();

                break;
            case EnemyState.Attack:

                HandleAttackType();

                break;
            case EnemyState.Dead:



                break;
        }
    }

    void HandleAttackType()
    {
        switch (enemyType)
        {
            case EnemyType.Melee:

                MeleeAttack();

                break;
            case EnemyType.Ranged:

                Debug.Log("Ranged attack");

                break;
            case EnemyType.Explosive:


                break;
        }
    }

    void Idle()
    {
        //Check for the player.

        anim.SetFloat("Vertical", 0);
        if (distanceToTarget <= viewDistance)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                enemyState = EnemyState.Chase;
            }
        }
    }

    void Chase()
    {
        anim.SetFloat("Vertical", 1);
        if (distanceToTarget > stoppingDistance)
        {
            transform.position = MoveToTarget();
            transform.rotation = RotateTowardsTarget(target.transform.position - transform.position);
        }
        else
        {
            enemyState = EnemyState.Attack;
        }

        if (distanceToTarget > viewDistance)
        {
            enemyState = EnemyState.Idle;
        }
    }

    void MeleeAttack()
    {
        anim.SetFloat("Vertical", 0);

        if (canAttack)
        {
            anim.Play("hit");
            //Logic on damage etc

            transform.rotation = RotateTowardsTarget(target.transform.position - transform.position);
            attackCooldownTimer = attackCooldown;
            canAttack = false;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
            if (attackCooldownTimer < 0)
            {
                canAttack = true;
                attackCooldownTimer = attackCooldown;
            }
        }

        if (distanceToTarget > stoppingDistance)
        {
            enemyState = EnemyState.Chase;
        }

    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * 0.5f);
        float stepAngleSize = viewAngle / stepCount;

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle) * viewDistance, Color.red);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        //angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmosSelected()
    {
        DrawFieldOfView();
    }

    Quaternion RotateTowardsTarget(Vector3 forward)
    {
        return Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(forward),
            rotateSpeed * Time.deltaTime);
    }

    Vector3 MoveToTarget()
    {
        return Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    bool IsChasing()
    {
        if (enemyState == EnemyState.Chase)
            return true;
        else
            return false;
    }

    float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    EnemyState GetEnemyState()
    {
        return enemyState;
    }

}

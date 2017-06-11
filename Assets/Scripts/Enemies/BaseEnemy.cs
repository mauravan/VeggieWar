using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IEnemy
{

    public int HitPoints=100;
    public int Damage=10;
    public float Range = 5.0f;

    public float timeBetweenAttacks = 1f;
    private float lastAttacked;
    public float rotationSpeed = 5f;




    //Userd for AI
    public Transform[] points;
    private int destPoint = 0;
    private bool enemyInSight;
    private bool closeForAttack;
    NavMeshAgent agent;
    public Transform target;
    private EnemyStates state;
    private enum EnemyStates
    {
        Patrolling,
        WalkingToPlayer,
        Attacking
    };


    //AUDIO
    public AudioClip a_Idle;
	public AudioClip a_Damage;
	public AudioClip a_Attack;
	AudioSource audioSource;

    


    // Use this for initialization
    void Start () {
        points = GameObject.FindGameObjectsWithTag("PatrolPoint").Select(go => go.transform).ToArray();
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = AudioControl.soundVolume;
        state = EnemyStates.Patrolling;
        GotoNextPoint();
        enemyInSight = false;
        closeForAttack = false;
        destPoint = new System.Random().Next(0, points.Length);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }



    void Patrol()
    {
        if (points.Length > 1)
        {
            agent.stoppingDistance = 0f;
            if (enemyInSight)
            {
                state = EnemyStates.WalkingToPlayer;
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();
            }
        } else
        {
            state = EnemyStates.WalkingToPlayer;
        }

    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = new System.Random().Next(0, points.Length);
    }



    // Update is called once per frame
    void Update () {
        lastAttacked -= Time.deltaTime;
        CheckIfCloseForAttack();
        CheckIfEnemyInSight();


        switch (state)
        {
            case EnemyStates.Patrolling:
                Patrol();
                break;
            case EnemyStates.WalkingToPlayer:
                WalkingToPlayer();
                break;
            case EnemyStates.Attacking:
                Attacking();
                break;
            default:
                break;
        }


        
        CheckIdleSound();
	    if (HitPoints <= 0)
	    {
            StateManager.Instance.NumberOfEnemies--;
	        Destroy(gameObject);
	    }
	}

    private void Attacking()
    {
        RotateTowards(target);
        if (closeForAttack && enemyInSight)
        {
            Attack();
        }
        else if (enemyInSight)
        {
            state = EnemyStates.WalkingToPlayer;
        }
        else
        {
            state = EnemyStates.Patrolling;
        }
    }

    

    private void WalkingToPlayer()
    {
        agent.stoppingDistance = 3f;
        if (!enemyInSight)
        {
            state = EnemyStates.Patrolling;
        } else
        {
            if (closeForAttack)
            {
                state = EnemyStates.Attacking;
            } else
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private void CheckIfCloseForAttack()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist < Range)
        {
            closeForAttack = true;
        }
        else
        {
            closeForAttack = false;
        }
    }

    private void CheckIfEnemyInSight()
    {
        RaycastHit hit;
        var direction = target.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform == target)
            {
                enemyInSight = true;
            }
            else
            {
                enemyInSight = false;
            }
        }
       
    }

    public int ReturnTotalDamage()
    {
        return Damage;
    }


    //Returns if dead or not
    public bool ApplyDamage(int dmg)
    {
		PlayDamageSound();
        if (HitPoints - dmg <= 0)
        {
            HitPoints -= dmg;
            return true;
        }
        HitPoints -= dmg;
        return false;
    }


    public void Attack()
    {
        if (lastAttacked <= 0)
        {
            //TODO: AttackMelee Animation and Hitboxes
            PlayAttackSound();
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), transform.forward, out hit, Range + 10))
            {
                if (hit.collider.tag == "Player")
                {
                    hit.transform.GetComponent<PlayerManager>().ApplyDamage(ReturnTotalDamage());
                }
            }
            lastAttacked = timeBetweenAttacks;
        }
    }

	private void PlayIdleSound(){
		if (a_Idle != null) {
			audioSource.clip = a_Idle;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	private void PlayDamageSound(){
		if (a_Damage != null) {
			audioSource.clip = a_Damage;
			audioSource.loop = false;
			audioSource.Play ();
		}
	}

	private void PlayAttackSound(){
		if (a_Attack != null) {
			audioSource.clip = a_Attack;
			audioSource.loop = false;
			audioSource.Play ();
		}
	}

	private void CheckIdleSound(){
		if (!audioSource.isPlaying)
			PlayIdleSound ();
	}
}

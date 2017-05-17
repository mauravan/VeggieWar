using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IEnemy
{

    public int HitPoints=100;
    public int Damage=10;
    public float Range = 2.0f;


    NavMeshAgent agent;
    public Transform target;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
	    if (HitPoints <= 0)
	    {
	        Destroy(gameObject);
	    }
	}

    public int ReturnTotalDamage()
    {
        return Damage;
    }

    public void ApplyDamage(int dmg)
    {
        HitPoints -= dmg;
    }

    public void Attack()
    {
        //TODO: AttackMelee Animation and Hitboxes
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Range))
        {
            if (hit.collider.tag == "Player")
            {
                hit.transform.GetComponent<PlayerManager>().ApplyDamage(ReturnTotalDamage());
            }
        }

    }
}

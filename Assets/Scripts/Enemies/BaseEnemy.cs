﻿using System.Collections;
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

    void OnTriggerEnter(Collider other)
    {
    }

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

    //Returns if dead or not
    public bool ApplyDamage(int dmg)
    {
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

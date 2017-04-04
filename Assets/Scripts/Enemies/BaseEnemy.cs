using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IEnemy
{

    public int HitPoints=100;
    public int Damage=10;
    public float Range = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        //TODO: Attack Animation and Hitboxes
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

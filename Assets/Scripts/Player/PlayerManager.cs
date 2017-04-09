using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : Singleton<PlayerManager> {

    //Stats
    public int HitPoints = 100;
    public int Damage = 5;
    public int Range = 2;


    public IWeapon CurrentWeapon { get; set; }



    public int ReturnTotalRange()
    {
        if (CurrentWeapon != null)
        {
            return Convert.ToInt32(Range + CurrentWeapon.GetRange());
        }
        return Range;
    }

    public int ReturnTotalDamage()
    {
        if (CurrentWeapon != null)
        {
            return Damage + CurrentWeapon.GetWeaponDamage();
        }
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
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, ReturnTotalRange()))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.transform.GetComponent<BaseEnemy>().ApplyDamage(ReturnTotalDamage());
            }
        }
           
    }
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (HitPoints <= 0)
	    {
	        //Todo: Kill Player
            //Load Home Screen
	    }
	    if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            Attack();
	    
	}
}

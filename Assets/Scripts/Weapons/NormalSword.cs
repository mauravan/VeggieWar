using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSword : MonoBehaviour, IWeapon
{

    public int Damage = 10;
    public float Range = 2.0f;

    public float GetRange()
    {
        return Range;
    }

    public int GetWeaponDamage()
    {
        return Damage;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

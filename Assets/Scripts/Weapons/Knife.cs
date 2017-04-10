using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour, IWeapon {

    public int Damage = 10;
    public float Range = 2.0f;

    public bool _IsThrowable = true;

    public float GetRange()
    {
        return Range;
    }

    public int GetWeaponDamage()
    {
        return Damage;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsThrowable()
    {
        return _IsThrowable;
    }
}

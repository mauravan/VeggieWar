﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon {

    public GameObject weaponPerfab;
    public Transform weaponSpawn;

    public int Damage = 10;
    public float Range = 2.0f;

    public bool _IsThrowable = true;

    private bool _hitting = false;

    private Animator _attackAnimator;

    public float GetRange()
    {
        return Range;
    }

    public int GetWeaponDamage()
    {
        return Damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_attackAnimator != null) &&  (other.tag == "Enemy") && (!_attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("Nothing")) && (_hitting))
        {
            other.transform.GetComponent<BaseEnemy>().ApplyDamage(PlayerManager.Instance.ReturnTotalDamage());
            Debug.Log(other.transform.GetComponent<BaseEnemy>().HitPoints);
            _hitting = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }


    // Use this for initialization
    void Start()
    {
        _attackAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsThrowable()
    {
        return _IsThrowable;
    }

    public float firingAngle = 45.0f;
    public float speed = 9.8f;

    IEnumerator IWeapon.Attack(bool rangeAttack)
    {
        if (rangeAttack)
        {
            _attackAnimator.SetTrigger("Throw");

            GameObject weaponClone = Instantiate(weaponPerfab, weaponSpawn.position, weaponSpawn.rotation) as GameObject;

            //Shooting
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Debug.Log(hit.collider.name);
                float targetDistance = Vector3.Distance(weaponClone.transform.position, hit.point);
                float projectile_Velocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / speed);

                float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
                float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

                float flightDuration = targetDistance / Vx;
                weaponClone.transform.rotation = Quaternion.LookRotation(hit.point - weaponClone.transform.position);

                

                float elapse_time = 0;

                while (elapse_time < flightDuration)
                {
                    weaponClone.transform.Translate(0, (Vy - (speed * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

                    elapse_time += Time.deltaTime;

                    yield return null;
                }

                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.transform.GetComponent<BaseEnemy>().ApplyDamage(PlayerManager.Instance.ReturnTotalDamage());
                    Debug.Log(hit.collider.transform.GetComponent<BaseEnemy>().HitPoints);
                    Destroy(weaponClone);
                }
            }
            Destroy(weaponClone, 2.0f);
        }
        else
        {
            _attackAnimator.SetTrigger("Hit_Short");
            _hitting = true;
        }
    }

    
}

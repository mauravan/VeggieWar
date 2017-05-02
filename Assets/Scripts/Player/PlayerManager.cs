using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerManager : Singleton<PlayerManager> {

    //Stats
    public int HitPoints = 100;
    private int _maxHitpoints;
    public int Damage = 5;
    public int Range = 2;

    public IWeapon CurrentWeapon { get; set; }

    public GameObject Crosshair;
    public GameObject Hitbar;

    private Animator _attackAnimator;

    //Used for hitbar calculations
    private Transform[] _hitbars;
    double[] limits;
    public float PercentageToSmallerHitbar = 0.99f;

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
        Debug.Log(HitPoints);

        //TODO: Regain Life?
        if (HitPoints >= limits[0])
        {
            _hitbars[0].localScale = new Vector3(_hitbars[0].localScale.x * 0.99f, _hitbars[0].localScale.y * 0.99f, 1);
        }
        if (HitPoints <= limits[0] && HitPoints >= limits[1])
        {
            _hitbars[0].localScale = new Vector3(0, 0, 0);
            _hitbars[1].localScale = new Vector3(_hitbars[1].localScale.x * PercentageToSmallerHitbar, _hitbars[1].localScale.y * PercentageToSmallerHitbar, 1);
        }
        if (HitPoints <= limits[1] && HitPoints >= limits[2])
        {
            _hitbars[1].localScale = new Vector3(0, 0, 0);
            _hitbars[2].localScale = new Vector3(_hitbars[2].localScale.x * PercentageToSmallerHitbar, _hitbars[2].localScale.y * PercentageToSmallerHitbar, 1);
        }
        if (HitPoints <= limits[2] && HitPoints >= limits[3])
        {
            _hitbars[2].localScale = new Vector3(0, 0, 0);
            _hitbars[3].localScale = new Vector3(_hitbars[3].localScale.x * PercentageToSmallerHitbar, _hitbars[3].localScale.y * PercentageToSmallerHitbar, 1);
        }
        if (HitPoints <= limits[3] && HitPoints >= limits[4])
        {
            _hitbars[3].localScale = new Vector3(0, 0, 0);
            _hitbars[4].localScale = new Vector3(_hitbars[4].localScale.x * PercentageToSmallerHitbar, _hitbars[4].localScale.y * PercentageToSmallerHitbar, 1);
        }
        if (HitPoints <= limits[4]) //Death
        {
            _hitbars[4].localScale = new Vector3(0, 0, 0);
            //Todo: Kill Player
            //Load Home Screen
        }
    }

    public void AttackMelee()
    {
        //TODO: AttackMelee Animation and Hitboxes
        _attackAnimator.SetTrigger("Hit_Short");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, ReturnTotalRange()))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.transform.GetComponent<BaseEnemy>().ApplyDamage(ReturnTotalDamage());
            }
        }
           
    }

    private void AttackRange()
    {
        //TODO: Attack Range Animation and Hitboxes
        _attackAnimator.SetTrigger("Throw");
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
    void Start ()
    {
        _attackAnimator = transform.GetComponentInChildren<Animator>();

        Crosshair.SetActive(false);
        _maxHitpoints = HitPoints;
        CurrentWeapon = transform.GetComponent<Knife>(); //TODO: Not set statically
        _hitbars = new Transform[] {    Hitbar.transform.FindChild("Hitbar"),
                                        Hitbar.transform.FindChild("Hitbar_1"),
                                        Hitbar.transform.FindChild("Hitbar_2"),
                                        Hitbar.transform.FindChild("Hitbar_3"),
                                        Hitbar.transform.FindChild("Hitbar_4")  };

        limits = new double[] {             _maxHitpoints * 0.8,
                                            _maxHitpoints * 0.6,
                                            _maxHitpoints * 0.4,
                                            _maxHitpoints * 0.2,
                                            _maxHitpoints * 0.0 };
    }
	
	// Update is called once per frame
	void Update () {

        //TODO: Check if currently attacking
        if (CurrentWeapon != null && CurrentWeapon.IsThrowable())
        {
            if (CrossPlatformInputManager.GetButton("Fire2"))
            {
                Crosshair.SetActive(true);
                if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                {
                    Debug.Log("Range");
                    AttackRange();
                }
            }
            else
            {
                Crosshair.SetActive(false);
                if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                {
                    Debug.Log("Melee");
                    AttackMelee();
                }
            }
            
        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                AttackMelee(); Debug.Log("Melee");
        }
	}
}

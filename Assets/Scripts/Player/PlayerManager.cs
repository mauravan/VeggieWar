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

    //Used for animation
    private bool isAttacking;
    public float attackTime;
    private float attackTimeCounter;

    //Used for hitbar calculations
    private Transform[] _hitbars;
    double[] limits;
    public float PercentageToSmallerHitbar = 0.99f;

	// Attack Audio
	public AudioClip soundThrow;
	public AudioClip soundAttack;
	private AudioSource audio;


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
            _hitbars[0].localScale = new Vector3(_hitbars[0].localScale.x * PercentageToSmallerHitbar, _hitbars[0].localScale.y * PercentageToSmallerHitbar, 1);
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
            StateManager.Instance.SetMomentaryState(States.STORY2);
        }
    }


    public void AttackMelee()
    {
		PlayAttackSound ();
        StartCoroutine(CurrentWeapon.Attack(false));
    }

    private void AttackRange()
    {
		PlayThrowSound ();
        StartCoroutine( CurrentWeapon.Attack(true) );
    }



    // Use this for initialization
    void Start ()
    {
        Crosshair.SetActive(false);
        _maxHitpoints = HitPoints;
        CurrentWeapon = transform.GetComponentInChildren<BaseWeapon>();
        _hitbars = new Transform[] {    Hitbar.transform.FindChild("Hitbar"),
                                        Hitbar.transform.FindChild("Hitbar_1"),
                                        Hitbar.transform.FindChild("Hitbar_2"),
                                        Hitbar.transform.FindChild("Hitbar_3"),
                                        Hitbar.transform.FindChild("Hitbar_4")  };

        for (int i = 0; i < _hitbars.Length; i++)
        {
            _hitbars[i].localScale = new Vector3(1, 1, 1);
        }
       
        limits = new double[] {             _maxHitpoints * 0.8,
                                            _maxHitpoints * 0.6,
                                            _maxHitpoints * 0.4,
                                            _maxHitpoints * 0.2,
                                            _maxHitpoints * 0.0 };
		InitSounds ();
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
                    AttackRange();
                }
            }
            else
            {
                Crosshair.SetActive(false);
                if (CrossPlatformInputManager.GetButtonDown("Fire1"))
                {
                    
                    AttackMelee();
                }
            }
            
        }
        else
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                AttackMelee();
            }
        }
	}
		
	private void InitSounds(){
		audio = GetComponent<AudioSource>();
		audio.volume = AudioControl.soundVolume  * 0.4f;
	}
	private void PlayThrowSound(){
		audio.clip = soundThrow;
		audio.Play();
	}
	private void PlayAttackSound(){
		audio.clip = soundAttack;
		audio.Play();
	}
}

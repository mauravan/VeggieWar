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

	public AudioClip a_Idle;
	public AudioClip a_Damage;
	public AudioClip a_Attack;
	AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
    }


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = AudioControl.soundVolume;
	}


	// Update is called once per frame
	void Update () {
		CheckIdleSound ();
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
        //TODO: AttackMelee Animation and Hitboxes
		PlayAttackSound();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Range))
        {
            if (hit.collider.tag == "Player")
            {
                hit.transform.GetComponent<PlayerManager>().ApplyDamage(ReturnTotalDamage());
            }
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

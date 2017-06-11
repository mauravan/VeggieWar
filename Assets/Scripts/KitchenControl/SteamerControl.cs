using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(AudioSource))]
public class SteamerControl : MonoBehaviour
{
    public Transform StartTransform;
    private Quaternion _endPosition;
    private Quaternion _startPosition;
    public Vector3 VectorToLerp;

    
    public GameObject DangerZoneCollider;

    public float Speed = 1.0F;
    public float TimeToKeepDoorOpen = 5f;

    private bool _isTriggerable = false; //If we are standing in the Triggerbox
    private bool _isTriggered = false; //If we pressed the action key
    private bool _doorOpen = false; // Keeps track of the door pos
    private float _lastTriggered; //Time since last triggered

	public AudioClip soundAmbient;
	public AudioClip soundActive;
	private AudioSource audio;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTriggerable = true;
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTriggerable = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        DangerZoneCollider.SetActive(false);
        _endPosition = StartTransform.rotation * Quaternion.Euler(VectorToLerp);
        _startPosition = StartTransform.rotation;
        _lastTriggered = Time.time;
		InitSound ();
    }
	
	// Update is called once per frame
	void Update () {
	    if (_doorOpen && Time.time - _lastTriggered >= TimeToKeepDoorOpen) // Door open and time exceded
	    {
	        _isTriggered = false;
	        ReturnDoorPosition();
	    }
	    if (_isTriggerable && !_doorOpen && CrossPlatformInputManager.GetButtonDown("Action")) // Player inside Trigger and pressed E
	    {
            //Open door
            _isTriggered = true;
	        _lastTriggered = Time.time;
			PlayActiveSound();
	    }
	    if (_isTriggered) //Open Door until Open
	    {
            StartTransform.rotation = Quaternion.Slerp(StartTransform.rotation, _endPosition, Time.time * Speed);
	        if (StartTransform.rotation == _endPosition)
	        {
	            _doorOpen = true;
                // Add damagecloud
                DangerZoneCollider.SetActive(true);
	        }
        }
        
    }

    private void ReturnDoorPosition()
    {
        StartTransform.rotation = Quaternion.Slerp(StartTransform.rotation, _startPosition, Time.time * Speed);
        if (StartTransform.rotation == _startPosition)
        {
            _doorOpen = false;
            DangerZoneCollider.SetActive(false);
			PlayAmbientSound ();
        }
    }

	private void InitSound(){
		audio = GetComponent<AudioSource>();
		audio.volume = AudioControl.soundVolume;
	}
	private void PlayActiveSound(){
		audio.Stop ();
		audio.clip = soundActive;
		audio.Play();
	}
	private void PlayAmbientSound(){
		audio.Stop ();
		audio.clip = soundAmbient;
		audio.Play();
	}


}

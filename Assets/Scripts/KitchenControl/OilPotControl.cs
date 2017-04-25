using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class OilPotControl : MonoBehaviour
{
    public Transform StartTransform;
    private Quaternion _endPosition;
    private Quaternion _startPosition;
    public Vector3 VectorToLerp;


    public GameObject DangerZoneCollider;
    public GameObject secondTrigger;


    public float Speed = 1.0F;
    public float TimeToKeepDoorOpen = 5f;

    private bool _isTriggerable = false; //If we are standing in the Triggerbox
    private bool _isTriggered = false; //If we pressed the action key
    private bool _doorOpen { get; set; } // Keeps track of the door pos
    private float _lastTriggered; //Time since last triggered

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Entered Console");
        _isTriggerable = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player Left Console");
        _isTriggerable = false;
    }

    // Use this for initialization
    void Start()
    {
        DangerZoneCollider.SetActive(false);
        _doorOpen = false;
        _endPosition = StartTransform.rotation * Quaternion.Euler(VectorToLerp);
        _startPosition = StartTransform.rotation;
        _lastTriggered = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_doorOpen && Time.time - _lastTriggered >= TimeToKeepDoorOpen) // Door open and time exceded
        {
            _isTriggered = false;
            ReturnDoorPosition();
        }
        if (_isTriggerable && !_doorOpen && CrossPlatformInputManager.GetButtonDown("Action") && !secondTrigger.GetComponent<OilPotControl>()._doorOpen) // Player inside Trigger and pressed E
        {
            Debug.Log("Triggered");
            //Open door
            _isTriggered = true;
            _lastTriggered = Time.time;

        }
        if (_isTriggered) //Open Door until Open
        {
            Debug.Log("Rotating forth");
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
        Debug.Log("Rotating back");
        StartTransform.rotation = Quaternion.Slerp(StartTransform.rotation, _startPosition, Time.time * Speed);
        if (StartTransform.rotation == _startPosition)
        {
            _doorOpen = false;
            DangerZoneCollider.SetActive(false);
        }
    }
}
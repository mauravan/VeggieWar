using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LevelTrigger : MonoBehaviour {

    private bool _isTriggerable;

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
    void Start () {
        _isTriggerable = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (_isTriggerable && CrossPlatformInputManager.GetButtonDown("Action") && !StateManager.Instance.LevelRunning) // Player inside Trigger and pressed E
        {
            StateManager.Instance.NextLevel();
        }

    }
}

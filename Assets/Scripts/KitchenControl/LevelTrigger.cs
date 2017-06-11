using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LevelTrigger : MonoBehaviour {

    private bool _isTriggerable;
    private GameObject infoBox;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !StateManager.Instance.LevelRunning)
        {
            infoBox.SetActive(true);
            _isTriggerable = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            infoBox.SetActive(false);
            _isTriggerable = false;
        }
    }


    // Use this for initialization
    void Start () {
        _isTriggerable = false;
        infoBox = GameObject.FindGameObjectWithTag("InfoBox");
        infoBox.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (_isTriggerable && CrossPlatformInputManager.GetButtonDown("Action") ) // Player inside Trigger and pressed E
        {
            StateManager.Instance.NextLevel();
            infoBox.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class KitchenState : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "Walk to door"))
        {
            StateManager.Instance.SetMomentaryState(States.TELEPORT_MENU);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "Start Fight"))
        {
            StateManager.Instance.SetMomentaryState(States.FIGHT);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

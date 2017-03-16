using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuState : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "MainMenu"))
        {
            StateManager.Instance.SetMomentaryState(States.MAIN_MENU);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "Load This"))
        {
            StateManager.Instance.SetMomentaryState(States.KITCHEN);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

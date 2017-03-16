using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightState : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "WinFight"))
        {
            StateManager.Instance.SetMomentaryState(States.KITCHEN);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "LoseFight"))
        {
            StateManager.Instance.SetMomentaryState(States.HOME_MENU);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

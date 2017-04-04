using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : MonoBehaviour {


    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "Start new Game"))
        {
            StateManager.Instance.SetMomentaryState(States.KITCHEN);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "Load Game"))
        {
            StateManager.Instance.SetMomentaryState(States.LOAD_MENU);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

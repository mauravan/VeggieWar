using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenuState : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "Go to Work"))
        {
            StateManager.Instance.SetMomentaryState(States.KITCHEN);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "Quit Game"))
        {
            Application.Quit();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

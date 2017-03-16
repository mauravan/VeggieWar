using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenuState : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 150, 30), "Home"))
        {
            StateManager.Instance.SetMomentaryState(States.HOME_MENU);
        }
        if (GUI.Button(new Rect(30, 70, 150, 30), "Shop"))
        {
            StateManager.Instance.SetMomentaryState(States.SHOP_MENU);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

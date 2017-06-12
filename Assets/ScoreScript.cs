using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {


    private void OnDestroy()
    {
        StateManager.Instance.SetMomentaryState(States.MAIN_MENU);
    }


    // Use this for initialization
    void Start () {
        transform.GetComponent<Text>().text = "Level: " + StateManager.Instance.Level;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *Kontextklasse, die die States verwaltet.
 */ 
public class StateManager : MonoBehaviour {

    private State momentaryState;


    public void SetMomentaryState(State s)
    {
        momentaryState = s;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(Input.mousePosition);
        }


    }
}


public interface State
{


}
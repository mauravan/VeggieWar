﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 *Kontextklasse, die die States verwaltet.
 */
public class StateManager : Singleton<StateManager> {

    private States _momentaryState;


    public void SetMomentaryState(States s)
    {
        _momentaryState = s;

        //StateSwitch
        switch (_momentaryState)
        {
            case States.MAIN_MENU:
                SceneManager.LoadScene("Main");
                break;
            case States.LOAD_MENU:
                SceneManager.LoadScene("LoadMenu");
                break;
            case States.KITCHEN:
                SceneManager.LoadScene("Kitchen");
                break;
            case States.FIGHT:
                //TODO: Just switch state - no new scene needed
                //SceneManager.LoadScene("HomeMenu");
                break;
            case States.PAUSE_MENU:
                //TODO: Do an overlay
                //SceneManager.LoadScene("HomeMenu");
                break;
            case States.TELEPORT_MENU:
                SceneManager.LoadScene("TeleportMenu");
                break;
            case States.SHOP_MENU:
                SceneManager.LoadScene("ShopMenu");
                break;
            case States.HOME_MENU:
                SceneManager.LoadScene("HomeMenu");
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this); //Keep alive between scenes
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(Input.mousePosition);
        }


    }
}


public enum States
{
    MAIN_MENU,
    LOAD_MENU,
    KITCHEN,
    FIGHT,
    PAUSE_MENU,
    TELEPORT_MENU,
    SHOP_MENU,
    HOME_MENU

}
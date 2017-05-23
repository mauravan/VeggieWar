using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 *Kontextklasse, die die States verwaltet.
 */
public class StateManager : Singleton<StateManager> {

    private States _momentaryState;


    public States GetMomentaryState()
    {
        return _momentaryState;
    }

    public void SetMomentaryState(States s)
    {
        States _prevState = _momentaryState;
        _momentaryState = s;

        //StateSwitch
        switch (_momentaryState)
        {
            case States.MAIN_MENU:
                SceneManager.LoadScene("MainMenu_VeggieWar");
                break;
            case States.STORY1:
                SceneManager.LoadScene("Story1");
                break;
            case States.KITCHEN:
                //Check if previous was pause or teleport, so that we can load the scene or just unpause
                if (!(_prevState == States.PAUSE_MENU) && !(_prevState == States.TELEPORT_MENU))
                {
                    SceneManager.LoadScene("Kitchen");
                }
                else
                {
                    //TODO: Close the overlays
                    Time.timeScale = 1;
                }
                break;
            case States.PAUSE_MENU:
                //TODO: Do an overlay
                Time.timeScale = 0;
                break;
            case States.TELEPORT_MENU:
                //TODO: Open Overlay
                Time.timeScale = 0;
                break;
            case States.STORY2:
                SceneManager.LoadScene("Story2");
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
       
    }
}


public enum States
{
    MAIN_MENU,
    STORY1,
    STORY2,
    KITCHEN,
    PAUSE_MENU,
    TELEPORT_MENU,
    FIGHT,
}
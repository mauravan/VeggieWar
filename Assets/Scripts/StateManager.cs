using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 *Kontextklasse, die die States verwaltet.
 */
public class StateManager : Singleton<StateManager> {

    private States _momentaryState;
    private int _level;
    public int NumberOfEnemies;
    private GameObject[] enemySpawns;
    public bool LevelRunning;
	public UnityStandardAssets.ImageEffects.SepiaTone pauseEffect;

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
					pauseEffect.enabled = false;
                    Time.timeScale = 1;
                }
                break;
            case States.PAUSE_MENU:
				pauseEffect.enabled = true;
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
        _level = 0;
        enemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawn");
        LevelRunning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (NumberOfEnemies == 0 && LevelRunning)
        {
            //Level Done
            _level++;
            LevelRunning = false;
        }
       
    }


    public void NextLevel()
    {
        int _numberOfEnemies = _level;

        while(_numberOfEnemies > 0)
        {
            for (int i = 0; i < enemySpawns.Length; i++)
            {
                enemySpawns[i].GetComponent<EnemySpawner>().SpawnEnemy();
                NumberOfEnemies++;
            }
            _numberOfEnemies--;
        }
        // NEW ROUND STARTED
        LevelRunning = true;
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
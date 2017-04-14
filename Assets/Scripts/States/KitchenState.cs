using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KitchenState : Singleton<KitchenState>
{
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if (!(StateManager.Instance.GetMomentaryState() == States.PAUSE_MENU) && !(StateManager.Instance.GetMomentaryState() == States.TELEPORT_MENU) && CrossPlatformInputManager.GetButtonDown("Cancel"))
	    {
            StateManager.Instance.SetMomentaryState(States.PAUSE_MENU);
	    }
	    else if (((StateManager.Instance.GetMomentaryState() == States.PAUSE_MENU) || (StateManager.Instance.GetMomentaryState() == States.TELEPORT_MENU)) && CrossPlatformInputManager.GetButtonDown("Cancel"))
	    {
            StateManager.Instance.SetMomentaryState(States.KITCHEN);
	    }
	   
		
	}

   
}

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KitchenState : MonoBehaviour
{

    private bool _isInOverlay = false;
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if (!_isInOverlay && CrossPlatformInputManager.GetButtonDown("Cancel"))
	    {
	        //TODO: Overlay Menu
	        _isInOverlay = true;
	        Time.timeScale = 0;
	    }
	    else if (_isInOverlay && CrossPlatformInputManager.GetButtonDown("Cancel"))
	    {
	        Time.timeScale = 1;
	        _isInOverlay = false;
	    }
	   
		
	}
}

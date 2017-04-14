using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (!(StateManager.Instance.GetMomentaryState() == States.FIGHT))
        {
            StateManager.Instance.SetMomentaryState(States.TELEPORT_MENU);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
       
       
    }

}

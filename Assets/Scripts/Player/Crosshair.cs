using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    private Rect _position;
    public Texture2D CrosshairTexture2D;

    // Use this for initialization
    void Start () {
        _position = new Rect((Screen.width - CrosshairTexture2D.width) / 2, (Screen.height - CrosshairTexture2D.height) / 2,
          CrosshairTexture2D.width, CrosshairTexture2D.height);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        GUI.DrawTexture(_position, CrosshairTexture2D);
    }
}

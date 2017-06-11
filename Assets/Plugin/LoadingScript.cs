using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {


	public float loadingTime;
	public Image loadingBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(loadingBar.fillAmount <=1){
		loadingBar.fillAmount += 1.0f / loadingTime * Time.deltaTime;
	}
}
}

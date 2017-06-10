using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float timeBetweenSpawns = 10f;
    private float counter; 

    public GameObject enemyPrefab;

    // Use this for initialization
    void Start () {
        counter = timeBetweenSpawns;
	}
	
	// Update is called once per frame
	void Update () {

        counter -= Time.deltaTime;

        Debug.Log(counter);

        if (counter <= 0)
        {
            GameObject weaponClone = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;

            counter = timeBetweenSpawns;
        }

        

    }
}

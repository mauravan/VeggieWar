using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private bool _insideTrigger = false;
    public int DamageToApplyPerFrame = 1;

    private List<BaseEnemy> _enemiesInsideTrigger;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Entered Damage");
            _insideTrigger = true;
        }
        if (other.tag == "Enemy")
        {
            _enemiesInsideTrigger.Add(other.transform.GetComponent<BaseEnemy>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Left Damage");
            _insideTrigger = false;
        }
        if (other.tag == "Enemy")
        {
            if (_enemiesInsideTrigger.Contains(other.transform.GetComponent<BaseEnemy>())) //TODO: Might not be necessairy, check if performance issue
            {
                _enemiesInsideTrigger.Remove(other.transform.GetComponent<BaseEnemy>());
            }
        }
    }

    // Use this for initialization
    void Start () {
        _enemiesInsideTrigger = new List<BaseEnemy>();
       
    }
	
	// Update is called once per frame
    void Update()
    {
        if (_insideTrigger)
        {
            PlayerManager.Instance.ApplyDamage(DamageToApplyPerFrame);
        }
        foreach (var enemy in _enemiesInsideTrigger) //Todo: Might block on large number of enemies: Do Async
        {
            enemy.ApplyDamage(DamageToApplyPerFrame);
        }
    }
       
}

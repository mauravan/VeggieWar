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
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            _insideTrigger = true;
        }
        if (other.tag == "Enemy")
        {
            _enemiesInsideTrigger.Add(other.transform.GetComponentInParent<BaseEnemy>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            _insideTrigger = false;
        }
        if (other.tag == "Enemy")
        {
            _enemiesInsideTrigger.Remove(other.transform.GetComponentInParent<BaseEnemy>());
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
       
        for (int i = _enemiesInsideTrigger.Count - 1; i >= 0; i--)
        {
            var dead = _enemiesInsideTrigger[i].ApplyDamage(DamageToApplyPerFrame);
            Debug.Log(_enemiesInsideTrigger[i].transform.GetComponent<BaseEnemy>().HitPoints);
            if (dead)
            {
                _enemiesInsideTrigger.RemoveAt(i);
            }
        }

    }
       
}

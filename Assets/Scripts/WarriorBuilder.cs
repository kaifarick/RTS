using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBuilder : MonoBehaviour
{
    [SerializeField]
    int SpawnTime;
    void Start()
    {
        StartCoroutine(SpwanHammer());
    }

    IEnumerator SpwanHammer()
    {
        while (true)
        {
            GameObject unit = PoolManager.Instance.GetPooledObject("HammerFriendly");
            unit.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            GameManager.Instance.AllFriendUnits.Add(unit);
            unit.SetActive(true);
            yield return new WaitForSeconds(SpawnTime);
        }
    }
}

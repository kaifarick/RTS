using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBuilder : MonoBehaviour
{
    [SerializeField]
    int SpawnTime;
    void Start()
    {
        StartCoroutine(SpwanCrossbow());
    }

    IEnumerator SpwanCrossbow()
    {
        while (true)
        {
            GameObject unit = PoolManager.Instance.GetPooledObject("CrossbowFriendly");
            unit.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            unit.SetActive(true);
            yield return new WaitForSeconds(SpawnTime);
            GameManager.Instance.AllFriendUnits.Add(unit);
        }
    }
}

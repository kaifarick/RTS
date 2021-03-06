﻿using System.Collections;
using UnityEngine;

public class HealthFountain : MonoBehaviour
{
    [SerializeField]
    int healthDistance;
    [SerializeField]
    int healthUp;
    [SerializeField]
    int speedHealth;

    LayerMask mask;
    Collider[] cols;
    void Start()
    {
        mask = LayerMask.GetMask("Player");
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        while (true)
        {
            cols = Physics.OverlapSphere(transform.position, healthDistance, mask.value);
            for (int i = 0; i < cols.Length; i++)
            {
                try
                {
                    cols[i].gameObject.GetComponent<HammerFriend>().Health += healthUp;
                }
                catch
                {
                    cols[i].gameObject.GetComponent<CrossbowFriendly>().Health += healthUp;
                }
            }
            yield return new WaitForSeconds(speedHealth);
        }
    }
}
        


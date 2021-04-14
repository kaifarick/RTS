using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{

    int range = 30;

    LayerMask mask;

    [SerializeField]
    int HealthUp;
    [SerializeField]
    int speedHealth;

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
            cols = Physics.OverlapSphere(transform.position, range, mask.value);
            for (int i = 0; i < cols.Length; i++)
            {
                try
                {
                    cols[i].gameObject.GetComponent<HammerFriend>().Health += HealthUp;
                }
                catch
                {
                    cols[i].gameObject.GetComponent<CrossbowFriendly>().Health += HealthUp;
                }
            }
            yield return new WaitForSeconds(speedHealth);
        }
    }
}
        


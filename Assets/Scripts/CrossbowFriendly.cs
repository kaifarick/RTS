using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowFriendly : Units

{
    int range = 100;
    float timeBetweenAtack;

    public GameObject Marker;

    LayerMask mask;

    void Start()
    {
        mask = LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        Health = GameManager.Instance.units.Health + UiManager.Instance.UpWarrior;
        Damage = GameManager.Instance.units.Damage + UiManager.Instance.UpWarrior;
    }


    void Update()
    {
        var cols = Physics.OverlapSphere(transform.position, range, mask.value);
        float dist = Mathf.Infinity;

        try
        {
            Collider currentCollider = cols[0];

            foreach (Collider col in cols)
            {
                float currentDist = Vector3.Distance(transform.position, col.transform.position);
                if (currentDist < dist)
                {
                    currentCollider = col;
                    dist = currentDist;
                }
            }

            if (dist < 10f)
            {

                if (timeBetweenAtack <= 0)
                {
                    try
                    {
                        HammerEnemy units = currentCollider.gameObject.GetComponent<HammerEnemy>();
                        units.GetDamage(Damage);
                    }
                    catch
                    {
                        CrossbowEnemy crossbow = currentCollider.gameObject.GetComponent<CrossbowEnemy>();
                        crossbow.GetDamage(Damage);
                    }
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
        catch
        {
            return;
        }
    }
}


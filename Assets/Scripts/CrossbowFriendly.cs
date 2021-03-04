using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowFriendly : Units

{
    private Transform playerPos;
    private Transform castle;

    CrossbowEnemy crossbowEnemy;

    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;

    [SerializeField]
    private float Speed;
    void Start()
    {
        castle = GameObject.Find("MainCastle").transform;
        mask = LayerMask.GetMask("Enemy");
        crossbowEnemy = GetComponent<CrossbowEnemy>();
    }

    private void OnEnable()
    {
        Health = 100 + UiManager.Instance.UpWarrior;
        Damage = 100 + UiManager.Instance.UpWarrior;
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

            playerPos = currentCollider.gameObject.transform;

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


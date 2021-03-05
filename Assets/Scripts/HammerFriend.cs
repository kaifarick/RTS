﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerFriend : Units

{
    private Transform playerPos;
    private Transform castle;


    HammerEnemy hammer;

    int startHealth;
    int startDamage;
    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;


    void Start()
    {

        castle = GameObject.Find("MainCastle").transform;
        mask = LayerMask.GetMask("Enemy");
        hammer = GetComponent<HammerEnemy>();
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

            playerPos = currentCollider.gameObject.transform;

            if (dist < 1.6f)
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
                        CrossbowEnemy crossbowEnemy = currentCollider.gameObject.GetComponent<CrossbowEnemy>();
                        crossbowEnemy.GetDamage(Damage);
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

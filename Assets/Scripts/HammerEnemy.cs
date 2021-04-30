using System.Collections;
using UnityEngine;

public class HammerEnemy : Units
{
    Transform playerPos;
    Castle castle;

    private void Start()
    {    
       castle = FindObjectOfType<Castle>();
        AttackParametrs(AttackDistance: 1.7f, "Player");
    }


    void Update()
    {
        var cols = Physics.OverlapSphere(transform.position, range, mask.value);
        float searchDistance = Mathf.Infinity;

        try
        {
            Collider currentCollider = cols[0];

            foreach (Collider col in cols)
            {
                float currentDist = Vector3.Distance(transform.position, col.transform.position);
                if (currentDist < searchDistance)
                {
                    currentCollider = col;
                    searchDistance = currentDist;
                }
            }

            playerPos = currentCollider.gameObject.transform;

            if (searchDistance > AttackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, MoveSpeed * Time.deltaTime);
            }
            else
            {
                if (timeBetweenAtack <= 0)
                {
                    try
                    {
                        HammerFriend units = currentCollider.gameObject.GetComponent<HammerFriend>();
                        units.GetDamage(Damage);
                        UiManager.Instance.UnitUiRefresh();
                    }
                    catch
                    {
                        CrossbowFriendly crossbow = currentCollider.gameObject.GetComponent<CrossbowFriendly>();
                        crossbow.GetDamage(Damage);
                        UiManager.Instance.UnitUiRefresh();
                    }
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
        catch
        {
            float currentDist = Vector3.Distance(gameObject.transform.position, castle.transform.position);
            if (currentDist > AttackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.transform.position, MoveSpeed * Time.deltaTime); ;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Castle>())
        {
            StartCoroutine(CastleDamage());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Castle>())
        {
            StopAllCoroutines();
        }
    }
    private IEnumerator CastleDamage()
    {
        while (true)
        {
            castle.Health -= Damage;
            UiManager.Instance.CastleTXTrefresh();
            yield return new WaitForSeconds(2);
        }
    }
}



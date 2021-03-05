using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HammerEnemy : Units
{
    private Transform playerPos;
    Castle castle;

    HammerEnemy hammer;

    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;
    void Start()
    {
        castle = FindObjectOfType<Castle>();
        mask = LayerMask.GetMask("Player");
        hammer = GetComponent<HammerEnemy>();
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

            if (dist > 1.6f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, GameManager.Instance.units.MoveSpeed * Time.deltaTime);
            }
            else
            {
                if (timeBetweenAtack <= 0)
                {
                    try
                    {
                        HammerFriend units = currentCollider.gameObject.GetComponent<HammerFriend>();
                        units.GetDamage(Damage);
                    }
                    catch
                    {
                        CrossbowFriendly crossbow = currentCollider.gameObject.GetComponent<CrossbowFriendly>();
                        crossbow.GetDamage(Damage);
                    }
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
        catch
        {
            float currentDist = Vector3.Distance(gameObject.transform.position, castle.transform.position);
            if (currentDist > 3.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.transform.position, GameManager.Instance.units.MoveSpeed * Time.deltaTime); ;
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
        Castle castle = FindObjectOfType<Castle>();
        while (true)
        {
            castle.health -= Damage;
            UiManager.Instance.CastleHealthTXT.text = "Castle" + castle.health.ToString();
            castle.RestartGames();
            yield return new WaitForSeconds(2);
        }
    }
}



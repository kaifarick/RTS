using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CrossbowEnemy : Units
{
    private Transform playerPos;
    private Transform castle;

    CrossbowEnemy crossbowEnemy;
    Castle castle1;

    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;

    [SerializeField]
    private float Speed;
    void Start()
    {
        castle1 = FindObjectOfType<Castle>();
        castle = GameObject.Find("MainCastle").transform;
        mask = LayerMask.GetMask("Player");
        crossbowEnemy = GetComponent<CrossbowEnemy>();
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

            if (dist > 10f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, Speed * Time.deltaTime);
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
            if (currentDist > 10f)
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.position, Speed * Time.deltaTime);
            }

            else
            {
                if (timeBetweenAtack <= 0)
                {
                    castle1.health -= Damage;
                    castle1.RestartGames();
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
    }   
}

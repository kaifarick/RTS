using UnityEngine;

public class CrossbowEnemy : Units
{
    private Transform playerPos;

    Castle castle;
    LayerMask mask;

    int range = 100;
    float timeBetweenAtack;

    void Start()
    {
        castle = FindObjectOfType<Castle>();
        mask = LayerMask.GetMask("Player");
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
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, MoveSpeed * Time.deltaTime);
            }
            else
            {
                if (timeBetweenAtack <= 0)
                {
                    try
                    {
                        HammerFriend hammerFriend = currentCollider.gameObject.GetComponent<HammerFriend>();
                        hammerFriend.GetDamage(Damage);
                    }
                    catch
                    {
                        CrossbowFriendly crossbowFriendly = currentCollider.gameObject.GetComponent<CrossbowFriendly>();
                        crossbowFriendly.GetDamage(Damage);
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
                transform.position = Vector3.MoveTowards(transform.position, castle.transform.position, MoveSpeed * Time.deltaTime);
            }

            else
            {
                if (timeBetweenAtack <= 0)
                {
                    castle.health -= Damage;
                    UiManager.Instance.CastleTXTrefresh();
                    castle.RestartGames();
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
    }   
}

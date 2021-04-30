using UnityEngine;

public class CrossbowEnemy : Units
{
    Transform playerPos;
    Castle castle;

    void Start()
    {
        castle = FindObjectOfType<Castle>();
        AttackParametrs(AttackDistance: 10f, "Player");
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
            if (currentDist > AttackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, castle.transform.position, MoveSpeed * Time.deltaTime);
            }

            else
            {
                if (timeBetweenAtack <= 0)
                {
                    castle.Health -= Damage;
                    UiManager.Instance.CastleTXTrefresh();
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
    }   
}

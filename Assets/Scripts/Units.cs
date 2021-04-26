using UnityEngine;

public class Units : MonoBehaviour
{
    int range = 100;
    float timeBetweenAtack;
    LayerMask mask;

    public int Health;
    public int Damage;
    public int MoveSpeed;

    [HideInInspector]
    public GameObject TargetPosition;
    public int StartHealth { get; private set; }
    public int StartDamage { get; private set; }

    public void StartParametrs()
    {
        StartDamage = Damage;
        StartHealth = Health;
    }


    public void GetDamage(int damage)
    {
        Health -= damage;
        Dead();
    }

    public void Attack(float AttackDistance)
    {
        mask = LayerMask.GetMask("Enemy");
        var cols = Physics.OverlapSphere(transform.position, range, mask.value);
        float dist = Mathf.Infinity;

        if (cols.Length > 0)
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

            if (dist < AttackDistance)
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
    }

    public void SaveTargetPosition()
    {
        float random = Random.Range(-2f, 2f);
        Vector3 vector3 = new Vector3(GameManager.Instance.target.transform.position.x + random, GameManager.Instance.target.transform.position.y, GameManager.Instance.target.transform.position.z + random);

        TargetPosition = PoolManager.Instance.GetPooledObject("Target");
        TargetPosition.SetActive(true);
        TargetPosition.transform.position = vector3;
    }
    public void Dead()
    {
        if(Health <= 0) gameObject.SetActive(false);
    }
}

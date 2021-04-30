using UnityEngine;

public class Units : MonoBehaviour
{
    protected int range = 100;
    protected float timeBetweenAtack;
    protected float AttackDistance;

    protected LayerMask mask;
    protected GameObject TargetPosition;

    public int Health;
    public int Damage;
    public int MoveSpeed;
    protected int StartHealth { get; private set; }
    protected int StartDamage { get; private set; }

    protected void StartParametrs()
    {
        StartDamage = Damage;
        StartHealth = Health;
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        Dead();
    }

    protected void AttackParametrs(float AttackDistance, string layerMask)
    {
        this.AttackDistance = AttackDistance;
        mask = LayerMask.GetMask(layerMask);
    }



    protected virtual void Attack(float AttackDistance, string layerMask)
    {
        mask = LayerMask.GetMask(layerMask);
        var cols = Physics.OverlapSphere(transform.position, range, mask.value);
        float searchDistance = Mathf.Infinity;

        if (cols.Length > 0)
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

            if (searchDistance < AttackDistance)
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

    protected void SaveTargetPosition()
    {
        float random = Random.Range(-2f, 2f);
        Vector3 vector3 = new Vector3(GameManager.Instance.target.transform.position.x + random, GameManager.Instance.target.transform.position.y, GameManager.Instance.target.transform.position.z + random);

        TargetPosition = PoolManager.Instance.GetPooledObject("Target");
        TargetPosition.SetActive(true);
        TargetPosition.transform.position = vector3;
    }
    void Dead()
    {
        if(Health <= 0) gameObject.SetActive(false);
    }
}

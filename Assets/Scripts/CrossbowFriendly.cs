using UnityEngine;

public class CrossbowFriendly : Units
{
    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;
    void Awake()
    {
        StartParametrs();
        mask = LayerMask.GetMask("Enemy");
    }

    private void OnEnable()
    {
        Health = StartHealth + UiManager.Instance.UpWarrior;
        Damage = StartDamage + UiManager.Instance.UpWarrior;
    }

    private void GroupMove()
    {
        if (GameManager.Instance.target)
        {
            foreach (GameObject gameObject in GameManager.Instance.GroupSelected)
            {
                if (gameObject == this.gameObject)
                {
                    transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.target.position, MoveSpeed * Time.deltaTime);
                    transform.LookAt(GameManager.Instance.target);
                }
            }

        }
    }

    void Update()
    {
        GroupMove();

        var cols = Physics.OverlapSphere(transform.position, range, mask.value);
        float dist = Mathf.Infinity;

        if (cols.Length>0) 
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
                        CrossbowEnemy units = currentCollider.gameObject.GetComponent<CrossbowEnemy>();
                        units.GetDamage(Damage);
                    }
                    timeBetweenAtack = 2f;
                }
                else timeBetweenAtack -= Time.deltaTime;
            }
        }
    }
}


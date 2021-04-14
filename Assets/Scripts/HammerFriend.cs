using UnityEngine;

public class HammerFriend : Units

{
    Transform playerPos;

    int range = 100;
    float timeBetweenAtack;

    LayerMask mask;
    CapsuleCollider capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
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
                    capsuleCollider.isTrigger = true;
                    transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.target.position, MoveSpeed * Time.deltaTime);
                    transform.LookAt(GameManager.Instance.target);
                }
            }
        }
        else capsuleCollider.isTrigger = false;
    }

    void Update()
    {
        GroupMove();

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
    }
}

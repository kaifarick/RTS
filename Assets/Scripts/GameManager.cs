using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int friendMoveSpeed;
    [SerializeField]
    int hammerCost;
    [SerializeField]
    int crossbowCost;
    [SerializeField]
    int callWarriors;
    [SerializeField]
    int moneyFromWave;
    [SerializeField]
    int timeSpawn;

    [SerializeField]
    private GameObject dot;
    private GameObject unit;
    private Transform target;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectTarget() ;
        if (Input.GetMouseButtonDown(1)) SetTarget();
        if (target) Move();
    }

    IEnumerator SpawnEnemy()
    { 
        int healthUp = 5;
        int damageUp = 5;

        while (true)
        {
            for (int i = 0; i < callWarriors; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("Hammer");
                Enemy.GetComponent<HammerEnemy>().Health = 100;
                Enemy.GetComponent<HammerEnemy>().Damage = 25;
                Enemy.SetActive(true);
                Enemy.GetComponent<HammerEnemy>().Health += healthUp;
                Enemy.GetComponent<HammerEnemy>().Damage += damageUp;

            }

            for (int i = 0; i < callWarriors; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("Crossbow");
                Enemy.GetComponent<CrossbowEnemy>().Health = 100;
                Enemy.GetComponent<CrossbowEnemy>().Damage = 25;
                Enemy.SetActive(true);
                Enemy.GetComponent<CrossbowEnemy>().Health += healthUp;
                Enemy.GetComponent<CrossbowEnemy>().Damage += damageUp;

            }
            callWarriors += 2;
            healthUp += 5;
            damageUp += 5;
            UiManager.Instance.AddMoney(moneyFromWave);
            yield return new WaitForSeconds(timeSpawn);
        }
    }

    void SetTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (target) Destroy(target.gameObject);

            GameObject newTarget = Instantiate(dot, hit.point, Quaternion.identity);
            target = newTarget.transform;
        }
    }

    void SelectTarget()
    {
        if (target)
        {
            Destroy(target.gameObject);
        }
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            HammerFriend warrior = hit.collider.GetComponent<HammerFriend>();
            CrossbowFriendly crossbowFriendly = hit.collider.GetComponent<CrossbowFriendly>();
            WarriorBuilder HammerBarrack = hit.collider.GetComponent<WarriorBuilder>();
            CrossbowBuilder CrossbowBarrack = hit.collider.GetComponent<CrossbowBuilder>();

            if (warrior)
            {
                unit = warrior.gameObject;

                UiManager.Instance.UnitHealth = unit.GetComponent<HammerFriend>().Health;
                UiManager.Instance.UnitDamage = unit.GetComponent<HammerFriend>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else if (crossbowFriendly)
            {
                unit = crossbowFriendly.gameObject;
                try
                {
                    UiManager.Instance.UnitHealth = unit.GetComponent<HammerFriend>().Health;
                    UiManager.Instance.UnitDamage = unit.GetComponent<HammerFriend>().Damage;
                }
                catch
                {
                    UiManager.Instance.UnitHealth = unit.GetComponent<CrossbowFriendly>().Health;
                    UiManager.Instance.UnitDamage = unit.GetComponent<CrossbowFriendly>().Damage;
                }

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }
            else if (HammerBarrack)
            {
                UiManager.Instance.BuyWarrior(hammerCost);

                if (UiManager.Instance.LeftMoney >= 0)
                {
                    GameObject friend = PoolManager.Instance.GetPooledObject("Player");
                    friend.gameObject.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                    friend.SetActive(true);
                    UiManager.Instance.UnitUi.SetActive(false);
                }
            }

            else if (CrossbowBarrack)
            {
                UiManager.Instance.BuyWarrior(crossbowCost);

                if (UiManager.Instance.LeftMoney >= 0)
                {
                    GameObject friend = PoolManager.Instance.GetPooledObject("CrossbowFriendly");
                    friend.gameObject.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                    friend.SetActive(true);
                    UiManager.Instance.UnitUi.SetActive(false);
                }
            }
            else
            {
                unit = null;
                UiManager.Instance.UnitUi.SetActive(false);
            }
        }
    }

    private void Move()
    {
        if (unit == null) return;
        unit.gameObject.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, target.position, friendMoveSpeed * Time.deltaTime);
        unit.gameObject.transform.LookAt(target);
    }

}

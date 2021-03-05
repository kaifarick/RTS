using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
    int healthUpEnemy;
    [SerializeField]
    int damageUpEnemy;

    public Units units;

    [SerializeField]
    private GameObject dot;
    private GameObject selectedUnit;
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
        
        while (true)
        {
            for (int i = 0; i < callWarriors; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("HammerEnemy");
                Enemy.GetComponent<HammerEnemy>().Health = units.Health;
                Enemy.GetComponent<HammerEnemy>().Damage = units.Damage;
                Enemy.SetActive(true);
                Enemy.GetComponent<HammerEnemy>().Health += healthUpEnemy;
                Enemy.GetComponent<HammerEnemy>().Damage += damageUpEnemy;

            }

            for (int i = 0; i < callWarriors; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("CrossbowEnemy");
                Enemy.GetComponent<CrossbowEnemy>().Health = units.Health;
                Enemy.GetComponent<CrossbowEnemy>().Damage = units.Damage;
                Enemy.SetActive(true);
                Enemy.GetComponent<CrossbowEnemy>().Health += healthUpEnemy;
                Enemy.GetComponent<CrossbowEnemy>().Damage += damageUpEnemy;

            }
            healthUpEnemy += healthUpEnemy;
            damageUpEnemy += damageUpEnemy;
            callWarriors += 2;
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
                selectedUnit = warrior.gameObject;

                UiManager.Instance.UnitHealth = selectedUnit.GetComponent<HammerFriend>().Health;
                UiManager.Instance.UnitDamage = selectedUnit.GetComponent<HammerFriend>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else if (crossbowFriendly)
            {
                selectedUnit = crossbowFriendly.gameObject;


                    UiManager.Instance.UnitHealth = selectedUnit.GetComponent<CrossbowFriendly>().Health;
                    UiManager.Instance.UnitDamage = selectedUnit.GetComponent<CrossbowFriendly>().Damage;
              

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }
            else if (HammerBarrack)
            {
                UiManager.Instance.BuyWarrior(hammerCost);

                if (UiManager.Instance.LeftMoney >= 0)
                {
                    GameObject friend = PoolManager.Instance.GetPooledObject("HammerFriendly");
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
                selectedUnit = null;
                UiManager.Instance.UnitUi.SetActive(false);
            }
        }
    }

    private void Move()
    {
        if (selectedUnit == null) return;
        selectedUnit.gameObject.transform.position = Vector3.MoveTowards(selectedUnit.gameObject.transform.position, target.position, units.MoveSpeed * Time.deltaTime);
        selectedUnit.gameObject.transform.LookAt(target);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int startFriendSpawn;
    [SerializeField]
    int callEnemyWarriors;
    [SerializeField]
    int timeSpawn;
    [SerializeField]
    int healthUpEnemy;
    [SerializeField]
    int damageUpEnemy;
    [SerializeField]
    int moneyFromWave;
    int moveSpeed;

    public List <GameObject> GroupSelected;
    public List <GameObject> AllFriendUnits;

    [SerializeField]
    private GameObject MovePoint;
    [SerializeField]
    private GameObject UnitUi;
    private GameObject unitFromUI;
    private GameObject selectedUnit;

    public Transform target;

    void Start()
    {
        FriendSpawn();
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) SelectTarget();
        if (Input.GetMouseButton(1)) SetTarget();
        if (target) MoveSingle();
    }

    void FriendSpawn()
    {
        for (int i = 0; i < startFriendSpawn; i++)
        {
            GameObject friend = PoolManager.Instance.GetPooledObject("HammerFriendly");
            friend.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            friend.SetActive(true);
            AllFriendUnits.Add(friend);            
        }

        for (int i = 0; i < startFriendSpawn; i++)
        {
            GameObject friend = PoolManager.Instance.GetPooledObject("CrossbowFriendly");
            friend.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            friend.SetActive(true);
            AllFriendUnits.Add(friend);
        }
    }
    IEnumerator SpawnEnemy()
    {
        CrossbowEnemy StartCrossbowParameter = PoolManager.Instance.GetPooledObject("CrossbowEnemy").GetComponent<CrossbowEnemy>();
        int crossbowHealth = StartCrossbowParameter.Health;
        int crossbowDamage = StartCrossbowParameter.Damage;
        HammerEnemy StartHammerParameter = PoolManager.Instance.GetPooledObject("HammerEnemy").GetComponent<HammerEnemy>();
        int hammerHealth = StartHammerParameter.Health;
        int hammerDamage = StartHammerParameter.Damage;

        int healthUpEnemy = 0;
        int damageUpEnemy = 0;
        int moneyFromWave = 0;
        
        while (true)
        {
            for (int i = 0; i < callEnemyWarriors; i++)
            {
                HammerEnemy Enemy = PoolManager.Instance.GetPooledObject("HammerEnemy").GetComponent<HammerEnemy>();

                Enemy.Health = hammerHealth + healthUpEnemy;
                Enemy.Damage = hammerDamage + damageUpEnemy;

                Enemy.gameObject.SetActive(true);
            }

            for (int i = 0; i < callEnemyWarriors; i++)
            {
                CrossbowEnemy Enemy = PoolManager.Instance.GetPooledObject("CrossbowEnemy").GetComponent<CrossbowEnemy>();

                Enemy.Health = crossbowHealth + healthUpEnemy;
                Enemy.Damage = crossbowDamage + damageUpEnemy;

                Enemy.gameObject.SetActive(true);
            }

            healthUpEnemy += this.healthUpEnemy;
            damageUpEnemy += this.damageUpEnemy;
            callEnemyWarriors += callEnemyWarriors;

            UiManager.Instance.AddMoney(moneyFromWave);
            yield return new WaitForSeconds(timeSpawn);

            moneyFromWave = this.moneyFromWave;
        }
    }

    void SetTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (target) Destroy(target.gameObject);

            GameObject newTarget = Instantiate(MovePoint, hit.point, Quaternion.identity);
            target = newTarget.transform;
        }
    }

    void SelectTarget()
    {
        if (target)
        {
            Destroy(target.gameObject);
        }

        Behaviour halo;
        if (unitFromUI != null)
        {
            halo = (Behaviour)unitFromUI.GetComponent("Halo");
            halo.enabled = false;
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            HammerFriend hammerFriend = hit.collider.GetComponent<HammerFriend>();
            CrossbowFriendly crossbowFriendly = hit.collider.GetComponent<CrossbowFriendly>();
            HammerEnemy hammerEnemy = hit.collider.GetComponent<HammerEnemy>();
            CrossbowEnemy crossbowEnemy = hit.collider.GetComponent<CrossbowEnemy>();

            if (hammerFriend)
            {
                selectedUnit = hammerFriend.gameObject;
                unitFromUI = hammerFriend.gameObject;
                moveSpeed = hammerFriend.MoveSpeed;

                halo = (Behaviour)unitFromUI.GetComponent("Halo");
                halo.enabled = true;

                UiManager.Instance.UnitHealth = unitFromUI.GetComponent<HammerFriend>().Health;
                UiManager.Instance.UnitDamage = unitFromUI.GetComponent<HammerFriend>().Damage;
                UiManager.Instance.UnitUiRefresh();

                UnitUi.SetActive(true);
            }

            else if (crossbowFriendly)
            {
                selectedUnit = crossbowFriendly.gameObject;
                unitFromUI = crossbowFriendly.gameObject;
                moveSpeed = crossbowFriendly.MoveSpeed;

                halo = (Behaviour)unitFromUI.GetComponent("Halo");
                halo.enabled = true;

                UiManager.Instance.UnitHealth = unitFromUI.GetComponent<CrossbowFriendly>().Health;
                UiManager.Instance.UnitDamage = unitFromUI.GetComponent<CrossbowFriendly>().Damage;
                UiManager.Instance.UnitUiRefresh();

                UnitUi.SetActive(true);
            }

            else if (hammerEnemy)
            {
                unitFromUI = hammerEnemy.gameObject;
                halo = (Behaviour)unitFromUI.GetComponent("Halo");
                halo.enabled = true;

                UiManager.Instance.UnitHealth = unitFromUI.GetComponent<HammerEnemy>().Health;
                UiManager.Instance.UnitDamage = unitFromUI.GetComponent<HammerEnemy>().Damage;
                UiManager.Instance.UnitUiRefresh();

                UnitUi.SetActive(true);
            }

            else if (crossbowEnemy)
            {
                unitFromUI = crossbowEnemy.gameObject;
                halo = (Behaviour)unitFromUI.GetComponent("Halo");
                halo.enabled = true;

                UiManager.Instance.UnitHealth = unitFromUI.GetComponent<CrossbowEnemy>().Health;
                UiManager.Instance.UnitDamage = unitFromUI.GetComponent<CrossbowEnemy>().Damage;
                UiManager.Instance.UnitUiRefresh();

                UnitUi.SetActive(true);
            }

            else
            {
                if (unitFromUI != null)
                {
                    halo = (Behaviour)unitFromUI.GetComponent("Halo");
                    halo.enabled = false;
                }
                selectedUnit = null;
                UnitUi.SetActive(true);
            }
        }
    }

    private void MoveSingle()
    {
        if (selectedUnit == null) return;
        selectedUnit.gameObject.transform.position = Vector3.MoveTowards(selectedUnit.gameObject.transform.position, target.position, moveSpeed * Time.deltaTime);
        selectedUnit.gameObject.transform.LookAt(target);
    }
}

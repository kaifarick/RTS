using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int StartFriendSpawn;
    [SerializeField]
    int callEnemyWarriors;
    [SerializeField]
    int moneyFromWave;
    [SerializeField]
    int timeSpawn;
    [SerializeField]
    int healthUpEnemy;
    [SerializeField]
    int damageUpEnemy;

    public Units units;

    public List <GameObject> GroupSelected;
    public List <GameObject> AllFriendUnits;

    [HideInInspector]
    public GameObject uiUnit;
    [HideInInspector]
    public GameObject selectedUnit;
    public GameObject MovePoint;

    private Transform target;


    void Start()
    {
        FriendSpawn();
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectTarget();
        if (Input.GetMouseButtonDown(1)) SetTarget();
        Move();
    }

    void FriendSpawn()
    {
        for (int i = 0; i < StartFriendSpawn; i++)
        {
            GameObject friend = PoolManager.Instance.GetPooledObject("HammerFriendly");
            friend.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            friend.SetActive(true);
            AllFriendUnits.Add(friend);
            
        }

        for (int i = 0; i < StartFriendSpawn; i++)
        {
            GameObject friend = PoolManager.Instance.GetPooledObject("CrossbowFriendly");
            friend.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            friend.SetActive(true);
            AllFriendUnits.Add(friend);
        }
    }
            IEnumerator SpawnEnemy()
    { 
        
        while (true)
        {
            for (int i = 0; i < callEnemyWarriors; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("HammerEnemy");
                Enemy.GetComponent<HammerEnemy>().Health = units.Health;
                Enemy.GetComponent<HammerEnemy>().Damage = units.Damage;
                Enemy.SetActive(true);
                Enemy.GetComponent<HammerEnemy>().Health += healthUpEnemy;
                Enemy.GetComponent<HammerEnemy>().Damage += damageUpEnemy;

            }

            for (int i = 0; i < callEnemyWarriors; i++)
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
            callEnemyWarriors += 2;
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

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            HammerFriend hammerFriend = hit.collider.GetComponent<HammerFriend>();
            CrossbowFriendly crossbowFriendly = hit.collider.GetComponent<CrossbowFriendly>();
            HammerEnemy hammerEnemy = hit.collider.GetComponent<HammerEnemy>();
            CrossbowEnemy crossbowEnemy = hit.collider.GetComponent<CrossbowEnemy>();

            if (hammerFriend)
            {
                selectedUnit = hammerFriend.gameObject;
                uiUnit = hammerFriend.gameObject;
                uiUnit.GetComponent<HammerFriend>().Marker.SetActive(true);

                UiManager.Instance.UnitHealth = uiUnit.GetComponent<HammerFriend>().Health;
                UiManager.Instance.UnitDamage = uiUnit.GetComponent<HammerFriend>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else if (crossbowFriendly)
            {
                selectedUnit = crossbowFriendly.gameObject;
                uiUnit = crossbowFriendly.gameObject;
                uiUnit.GetComponent<CrossbowFriendly>().Marker.SetActive(true);

                UiManager.Instance.UnitHealth = uiUnit.GetComponent<CrossbowFriendly>().Health;
                UiManager.Instance.UnitDamage = uiUnit.GetComponent<CrossbowFriendly>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else if (hammerEnemy)
            {
                uiUnit = hammerEnemy.gameObject;
                uiUnit.GetComponent<HammerEnemy>().Marker.SetActive(true);

                UiManager.Instance.UnitHealth = uiUnit.GetComponent<HammerEnemy>().Health;
                UiManager.Instance.UnitDamage = uiUnit.GetComponent<HammerEnemy>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else if (crossbowEnemy)
            {
                uiUnit = crossbowEnemy.gameObject;
                uiUnit.GetComponent<CrossbowEnemy>().Marker.SetActive(true);

                UiManager.Instance.UnitHealth = uiUnit.GetComponent<CrossbowEnemy>().Health;
                UiManager.Instance.UnitDamage = uiUnit.GetComponent<CrossbowEnemy>().Damage;

                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.UnitUiRefresh();
            }

            else
            {
                switch (uiUnit.tag)
                {
                    case "HammerFriendly":
                        uiUnit.GetComponent<HammerFriend>().Marker.SetActive(false);
                        break;
                    case "CrossbowFriendly":
                        uiUnit.GetComponent<CrossbowFriendly>().Marker.SetActive(false);
                        break;
                    case "HammerEnemy":
                        uiUnit.GetComponent<HammerEnemy>().Marker.SetActive(false);
                        break;
                    case "CrossbowEnemy":
                        uiUnit.GetComponent<CrossbowEnemy>().Marker.SetActive(false);
                        break;


                }
                selectedUnit = null;
                UiManager.Instance.UnitUi.SetActive(false);
                
            }
        }
    }

    private void Move()
    {
        if (target)
        {
            MoveSingle();
            MoveGroup();
        }
    }

    private void MoveSingle()
    {
        if (selectedUnit == null) return;
        selectedUnit.gameObject.transform.position = Vector3.MoveTowards(selectedUnit.gameObject.transform.position, target.position, units.MoveSpeed * Time.deltaTime);
        selectedUnit.gameObject.transform.LookAt(target);
    }

    void MoveGroup()
    {
        if (GroupSelected == null) return;
        foreach (GameObject unit in GroupSelected)
        {
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, target.position, units.MoveSpeed * Time.deltaTime);
            unit.transform.LookAt(target);
        }

    }
}

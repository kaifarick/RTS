using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    int moveSpeed;

    [HideInInspector]
    public Transform target;

    [SerializeField]
    private GameObject dot;
    private GameObject unit;

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
        int pool = 10;
        int healthUp = 5;
        int damageUp = 5;

        while (true)
        {
            for (int i = 0; i < pool; i++)
            {
                GameObject Enemy = PoolManager.Instance.GetPooledObject("Hammer");
                Enemy.GetComponent<HammerEnemy>().Health = 100;
                Enemy.GetComponent<HammerEnemy>().Damage = 25;
                Enemy.SetActive(true);
                Enemy.GetComponent<HammerEnemy>().Health += healthUp;
                Enemy.GetComponent<HammerEnemy>().Damage += damageUp;

            }
            pool += 2;
            healthUp += 5;
            damageUp += 5;
            UiManager.Instance.AddMoney(100);
            yield return new WaitForSeconds(60f);
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
        if (target) Destroy(target.gameObject);
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            HammerFriend warrior = hit.collider.GetComponent<HammerFriend>();
            barrack barrack = hit.collider.GetComponent<barrack>();

            if (warrior)
            {
                unit = warrior.gameObject;
                UiManager.Instance.UnitUi.SetActive(true);
                UiManager.Instance.HealthCount = unit.GetComponent<HammerFriend>().Health;
                UiManager.Instance.DamagePower = unit.GetComponent<HammerFriend>().Damage;
                UiManager.Instance.UnitUiRefresh();
            }
            if (barrack)
            {
                GameObject friend = PoolManager.Instance.GetPooledObject("Player");
                friend.gameObject.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                friend.SetActive(true);
                UiManager.Instance.UnitUi.SetActive(false);
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
        unit.gameObject.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, target.position, moveSpeed * Time.deltaTime);
        unit.gameObject.transform.LookAt(target);
    }

}

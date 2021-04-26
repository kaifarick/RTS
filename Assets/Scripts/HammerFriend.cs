using UnityEngine;

public class HammerFriend : Units

{

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartParametrs();
    }
    void Start()
    {
        GameManager.Instance.SetTargetEvent.AddListener(TargetLogic);
    }

    private void OnEnable()
    {

        Health = StartHealth + UiManager.Instance.UpWarrior;
        Damage = StartDamage + UiManager.Instance.UpWarrior;
    }

    public void TargetLogic()
    {
        SaveTargetPosition();

    }

    private void GroupMove()
    {
        if (TargetPosition != null)
        {
            foreach (GameObject gameObject in GameManager.Instance.GroupSelected)
            {
                if (gameObject == this.gameObject)
                {
                    rb.mass = 80f;
                    transform.position = Vector3.MoveTowards(transform.position, TargetPosition.transform.position, MoveSpeed * Time.deltaTime);
                    transform.LookAt(TargetPosition.transform);
                }
            }
            if (transform.position == TargetPosition.transform.position)
            {

                TargetPosition.gameObject.SetActive(false);
                TargetPosition = null;
                rb.mass = 10f;
            }
        }
    }

    void Update()
    {
        GroupMove();
        Attack(AttackDistance: 1.6f);
    }
}

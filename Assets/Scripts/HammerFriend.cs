using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerFriend : Units

{
    public Rigidbody rigidbodyH;
    public Vector3 direction;
    private void Start()
    {
        rigidbodyH = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<HammerEnemy>())
        {
            GetDamage(collision.collider.GetComponent<HammerEnemy>().Damage);

            direction = rigidbodyH.velocity.normalized;
            rigidbodyH.AddForce(direction*50, ForceMode.Impulse);

            Dead();
        }
    }
    
}

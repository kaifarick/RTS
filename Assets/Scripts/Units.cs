using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int MoveSpeed;

    public void GetDamage(int damage)
    {
        Health -= damage;
        Dead();
    }

    public void Dead()
    {
        if(Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

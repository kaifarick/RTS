using UnityEngine;

public class Units : MonoBehaviour
{
    public int Health;
    public int Damage;
    public int MoveSpeed;
    public int StartHealth { get; private set; }
    public int StartDamage { get; private set; }

    public void StartParametrs()
    {
        StartDamage = Damage;
        StartHealth = Health;
    }

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

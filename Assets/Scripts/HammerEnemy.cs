using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HammerEnemy : Units
{
    public Vector3 direction;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<HammerFriend>())
        {
            GetDamage(collision.collider.GetComponent<HammerFriend>().Damage);
            Dead();
        }
        if (collision.collider.GetComponent<Castle>())
        {
            StartCoroutine(CastleDamage());
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Castle>())
        {

            StopAllCoroutines();
        }
    }

    private IEnumerator CastleDamage()
    {
        while (true)
        {
            Castle castle = FindObjectOfType<Castle>();
            {
                castle.health -= Damage;
                if (castle.health <= 0) SceneManager.LoadScene("SampleScene"); 
                yield return new WaitForSeconds(2);
            }
        }
    }
}


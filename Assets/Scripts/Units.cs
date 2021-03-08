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

        //switch (GameManager.Instance.uiUnit.tag)
        //{
        //    case "CrossbowEnemy":
        //        UiManager.Instance.UnitHealth = GameManager.Instance.uiUnit.GetComponent<CrossbowEnemy>().Health;
        //        UiManager.Instance.UnitDamage = GameManager.Instance.uiUnit.GetComponent<CrossbowEnemy>().Damage;
        //        break;
        //    case "HammerEnemy":
        //        UiManager.Instance.UnitHealth = GameManager.Instance.uiUnit.GetComponent<HammerEnemy>().Health;
        //        UiManager.Instance.UnitDamage = GameManager.Instance.uiUnit.GetComponent<HammerEnemy>().Damage;
        //        break;
        //    case "HammerFriendly":
        //        UiManager.Instance.UnitHealth = GameManager.Instance.uiUnit.GetComponent<HammerFriend>().Health;
        //        UiManager.Instance.UnitDamage = GameManager.Instance.uiUnit.GetComponent<HammerFriend>().Damage;
        //        break;
        //    case "CrossbowFriendly":
        //        UiManager.Instance.UnitHealth = GameManager.Instance.uiUnit.GetComponent<CrossbowFriendly>().Health;
        //        UiManager.Instance.UnitDamage = GameManager.Instance.uiUnit.GetComponent<CrossbowFriendly>().Damage;
        //        break;
        //}

        //UiManager.Instance.UnitUiRefresh();
    }

    public void Dead()
    {
        if(Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine.UI;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public Text UnitDamageTXT;
    public Text UnitHealthTXT;
    public Text MoneyTXT;
    public Text CastleHealthTXT;

    public Transform CrossbowArmTarget;
    public Transform HammerArmTarget;

    public GameObject Fountain;
    public GameObject CrossbowArm;
    public GameObject HammerArm;
    public GameObject UnitUi;

    public Castle CastleObj;

    public int hammerCost;
    public int MoneyCount;
    //[HideInInspector]
    public int UnitHealth;
    //[HideInInspector]
    public int UnitDamage;
    [HideInInspector]
    public int LeftMoney;
    [HideInInspector]
    public int UpWarrior;
    void Start()
    {
        MoneyCount -= GameManager.Instance.moneyFromWave;
        CastleObj = FindObjectOfType<Castle>();
        UiRefresh();
    }

    public void AddMoney(int number)
    {
        MoneyCount += number;
        MoneyTXT.text = "Money" + MoneyCount.ToString();

    }

    public void UnitUiRefresh()
    {
        UnitDamageTXT.text = "Damage " + UnitDamage.ToString();
        UnitHealthTXT.text = "Health " + UnitHealth.ToString();
    }

    public void UiRefresh()
    {
        CastleHealthTXT.text = "Castle" + CastleObj.health.ToString();
        MoneyTXT.text = "Money" + MoneyCount.ToString();
        UnitDamageTXT.text = "Damage " + UnitDamage.ToString();
        UnitHealthTXT.text = "Health " + UnitHealth.ToString();
    }

    public void CastleTXTrefresh()
    {
        CastleHealthTXT.text = "Castle" + CastleObj.health.ToString();
    }

    public void BuyWarrior()
    {
        LeftMoney = MoneyCount - hammerCost;
        if (LeftMoney >= 0)
        {
            MoneyCount -= hammerCost;
            MoneyTXT.text = "Money" + MoneyCount.ToString();

            GameObject friend = PoolManager.Instance.GetPooledObject("HammerFriendly");
            friend.gameObject.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            friend.SetActive(true);
        }
    }

    public void CreateHammerARMBtn()
    {
        LeftMoney = MoneyCount - 100;
        if (LeftMoney > 0)
        {
            for (int i = 0; i < HammerArmTarget.childCount; i++)
            {
                if (!HammerArmTarget.GetChild(i).gameObject.activeInHierarchy)
                {
                    HammerArmTarget.GetChild(i).gameObject.SetActive(true);
                    Instantiate(HammerArm, HammerArmTarget.GetChild(i).gameObject.transform.position, Quaternion.identity);
                    MoneyCount -= 100;
                    MoneyTXT.text = "Money" + MoneyCount.ToString();
                    return;
                }
            }
        }
    }

    public void CreateCrossbowARMBtn()
    {
        LeftMoney = MoneyCount - 100;
        if (LeftMoney > 0)
        {
            for (int i = 0; i < CrossbowArmTarget.childCount; i++)
            {
                if (!CrossbowArmTarget.GetChild(i).gameObject.activeInHierarchy)
                {
                    CrossbowArmTarget.GetChild(i).gameObject.SetActive(true);
                    Instantiate(CrossbowArm, CrossbowArmTarget.GetChild(i).gameObject.transform.position, Quaternion.identity);
                    MoneyCount -= 100;
                    MoneyTXT.text = "Money" + MoneyCount.ToString();
                    return;
                }
            }
        }
    }

    public void UpWarriorBtn()
    {
        LeftMoney = MoneyCount - 100;
        if (LeftMoney > 0)
        {
            UpWarrior += 10;
            MoneyCount -= 100;
            MoneyTXT.text = "Money" + MoneyCount.ToString();
        }
    }

    public void BuildFountain()
    {
        LeftMoney = MoneyCount - 100;
        if (LeftMoney > 0)
        {
            Instantiate(Fountain, new Vector3(2, 0, 20), Quaternion.identity);
            MoneyCount -= 100;
            MoneyTXT.text = "Money" + MoneyCount.ToString();
        }
    }
}

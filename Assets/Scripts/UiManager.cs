using UnityEngine.UI;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    [SerializeField]
    private Text UnitDamageTXT;
    [SerializeField]
    private Text UnitHealthTXT;
    [SerializeField]
    private Text MoneyTXT;
    [SerializeField]
    private Text CastleHealthTXT;

    [SerializeField]
    private Transform CrossbowArmTarget;
    [SerializeField]
    private Transform HammerArmTarget;

    [SerializeField]
    private GameObject Fountain;
    [SerializeField]
    private GameObject CrossbowArm;
    [SerializeField]
    private GameObject HammerArm;

    [SerializeField]
    private Castle CastleObj;

    [SerializeField]
    private int hammerCost;
    [SerializeField]
    private int hammerBuildingCost;
    [SerializeField]
    private int crossbowBuildingCost;
    [SerializeField]
    private int HealthFountainCost;
    [SerializeField]
    private int UpgradeFriendWarriors;
    [SerializeField]
    private int MoneyCount;

    [HideInInspector]
    public int UnitHealth;
    [HideInInspector]
    public int UnitDamage;
    [HideInInspector]
    public int UpWarrior;

    private int LeftMoney;
    void Start()
    {
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
        CastleHealthTXT.text = "Castle" + CastleObj.Health.ToString();
        MoneyTXT.text = "Money" + MoneyCount.ToString();
        UnitDamageTXT.text = "Damage " + UnitDamage.ToString();
        UnitHealthTXT.text = "Health " + UnitHealth.ToString();
    }

    public void CastleTXTrefresh()
    {
        CastleHealthTXT.text = "Castle" + CastleObj.Health.ToString();
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
        LeftMoney = MoneyCount - hammerBuildingCost;
        if (LeftMoney >= 0)
        {
            for (int i = 0; i < HammerArmTarget.childCount; i++)
            {
                if (!HammerArmTarget.GetChild(i).gameObject.activeInHierarchy)
                {
                    HammerArmTarget.GetChild(i).gameObject.SetActive(true);
                    Instantiate(HammerArm, HammerArmTarget.GetChild(i).gameObject.transform.position, Quaternion.identity);

                    MoneyCount -= hammerBuildingCost;
                    MoneyTXT.text = "Money" + MoneyCount.ToString();
                    return;
                }
            }
        }
    }

    public void CreateCrossbowARMBtn()
    {
        LeftMoney = MoneyCount - crossbowBuildingCost;
        if (LeftMoney >= 0)
        {
            for (int i = 0; i < CrossbowArmTarget.childCount; i++)
            {
                if (!CrossbowArmTarget.GetChild(i).gameObject.activeInHierarchy)
                {
                    CrossbowArmTarget.GetChild(i).gameObject.SetActive(true);
                    Instantiate(CrossbowArm, CrossbowArmTarget.GetChild(i).gameObject.transform.position, Quaternion.identity);

                    MoneyCount -= crossbowBuildingCost;
                    MoneyTXT.text = "Money" + MoneyCount.ToString();
                    return;
                }
            }
        }
    }

    public void UpWarriorBtn()
    {
        LeftMoney = MoneyCount - UpgradeFriendWarriors;
        if (LeftMoney >= 0)
        {
            UpWarrior += 10;
            MoneyCount -= UpgradeFriendWarriors;
            MoneyTXT.text = "Money" + MoneyCount.ToString();
        }
    }

    public void BuildFountain()
    {
        LeftMoney = MoneyCount - HealthFountainCost;
        if (LeftMoney >= 0)
        {
            Instantiate(Fountain, new Vector3(2, 0, 20), Quaternion.identity);
            MoneyCount -= HealthFountainCost;
            MoneyTXT.text = "Money" + MoneyCount.ToString();
        }
    }
}

using UnityEngine.UI;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public Text Damage;
    public Text Health;
    public Text Money;
    public Text CastleHealth;

    public GameObject UnitUi;
    public Castle CastleObj;

    public int UnitHealth;
    public int UnitDamage;
    public int MoneyCount;
    public int LeftMoney;
    void Start()
    {
        MoneyCount -= 100;
        UiRefresh();
    }

    public void AddMoney(int number)
    {
        MoneyCount += number;
        Money.text = "Money" + MoneyCount.ToString();

    }

    public void UnitUiRefresh()
    {
        Damage.text = "Damage " + UnitDamage.ToString();
        Health.text = "Health " + UnitHealth.ToString();
    }

    public void UiRefresh()
    {
        CastleHealth.text = "Castle" + CastleObj.health.ToString();
        Money.text = "Money" + MoneyCount.ToString();
        Damage.text = "Damage " + UnitDamage.ToString();
        Health.text = "Health " + UnitHealth.ToString();
    }

    public void BuyWarrior(int number)
    {
        LeftMoney = MoneyCount - number;
        if (LeftMoney >= 0)
        {
            MoneyCount -= number;
            Money.text = "Money" + MoneyCount.ToString();
        }
        else return;

    }
}

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

    public int HealthCount;
    public int DamagePower;


    public int MoneyStart = 200;
    void Start()
    {
        MoneyStart -= 100;
        UnitUiRefresh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int number)
    {
        MoneyStart += number;
        Money.text = "Money" + MoneyStart;

    }

    public void UnitUiRefresh()
    {
        CastleHealth.text = "Castle" + CastleObj.health;
        Money.text = "Money" + MoneyStart;
        Damage.text = "Damage "+DamagePower.ToString();
        Health.text = "Health "+HealthCount.ToString();
    }
}

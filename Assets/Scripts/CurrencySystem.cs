using UnityEngine;
using UnityEngine.UI;


public class CurrencySystem : MonoBehaviour
{
    public static CurrencySystem Instance;

    public int currencyAmount = 10;
    public Text MoneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
        UpdateMoneyUI();
    }

    public void SpendCurrency(int amount)
    {
        if (currencyAmount >= amount)
        {
            currencyAmount -= amount;
            UpdateMoneyUI();
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }

    private void UpdateMoneyUI()
    {
        if (MoneyText != null)
        {
            MoneyText.text = currencyAmount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = currencyAmount.ToString();
    }
}

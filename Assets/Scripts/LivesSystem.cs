using UnityEngine;
using UnityEngine.UI;

public class LivesSystem : MonoBehaviour
{
    public static LivesSystem Instance;

    public int Lives = 100;
    public Text LivesText;
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

    public void GainLive(int amount)
    {
        Lives += amount;
        UpdateMoneyUI();
    }

    public void LoseLive(int amount)
    {
        if (Lives >= amount)
        {
            Lives -= amount;
            UpdateMoneyUI();
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }

    private void UpdateMoneyUI()
    {
        if (LivesText != null)
        {
            LivesText.text = Lives.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        LivesText.text = Lives.ToString();
    }
}
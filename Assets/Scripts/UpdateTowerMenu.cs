using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTowerMenu : MonoBehaviour
{
    public GameObject UpgradeTurretCanva;
    public static UpdateTowerMenu Instance { get; private set; } // This declares the static property
    public Text TurretNameTag; // Changed to TextMeshProUGUI if you are using TMPro for UI text

    // Awake is called when the script instance is being loaded.
    // This is the ideal place to set up the Singleton pattern.
    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If no instance exists, set this one as the instance
            Instance = this;
            Debug.Log("UpdateTowerMenu: Instance set successfully for " + gameObject.name, this);

            // Optional: If you want this UI manager to persist across scene changes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists and it's not this one, destroy this duplicate
            Debug.LogWarning("UpdateTowerMenu: Duplicate instance found. Destroying " + gameObject.name + ". Existing instance: " + Instance.gameObject.name, this);
            Destroy(gameObject);
        }

        // Add a check for the UI canvas here too, for good measure
        if (UpgradeTurretCanva == null)
        {
            Debug.LogError("UpdateTowerMenu: UpgradeTurretCanva is NOT assigned in the Inspector! Please assign your UI Panel.", this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure the menu UI is hidden when the game starts
        if (UpgradeTurretCanva != null)
        {
            UpgradeTurretCanva.SetActive(false);
        }
    }

    // Update is called once per frame (not needed for this specific functionality)
    void Update()
    {
        // No specific update logic needed here for menu show/hide
    }

    public void ShowMenu()
    {
        if (UpgradeTurretCanva != null)
        {
            UpgradeTurretCanva.SetActive(true);
            Debug.Log("Upgrade menu displayed.", this);
        }
        else
        {
            Debug.LogError("UpdateTowerMenu: Cannot show menu, UpgradeTurretCanva is null in ShowMenu().", this);
        }
    }

    public void HideMenu()
    {
        if (UpgradeTurretCanva != null)
        {
            UpgradeTurretCanva.SetActive(false);
            Debug.Log("Upgrade menu hidden.", this);
        }
        else
        {
            Debug.LogError("UpdateTowerMenu: Cannot hide menu, UpgradeTurretCanva is null in HideMenu().", this);
        }
    }
}
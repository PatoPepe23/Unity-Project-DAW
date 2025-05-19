using UnityEngine;
using UnityEngine.UI;

public class UpdateTowerMenu : MonoBehaviour
{
    public GameObject UpgradeTurretCanva;
    public Text TurretNameTag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpgradeTurretCanva.SetActive(false);

        GameObject.Find("UpgradeTurretCanva").GetComponent<UpgradeTurretPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

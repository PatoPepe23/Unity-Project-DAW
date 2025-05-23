using UnityEngine;
using System.Linq;

public class TurretPlacer : MonoBehaviour
{
    public GameObject FireTower;
    public GameObject ElectroTower;
    public LayerMask Path;
    public LayerMask Turrethitbox;

    public GameObject FireTowerGhost;
    public GameObject electroTowerGhost;
    private GameObject ghostInstance;

    private GameObject turretselected;
    private bool TowerSelected;

    private Collider2D ghostCollider;

    private float currentZ = 0f;
    private float zStep = 0.1f;


    void Start()
    {
        TowerSelected = false;
       
    }

    public void fireTowerSelected()
    {
        if (CurrencySystem.Instance.currencyAmount >= 10)
        {
            

            TowerSelected = true;
            turretselected = FireTower;

            if (ghostInstance != null)
                Destroy(ghostInstance);

            ghostInstance = Instantiate(FireTowerGhost);
        }
        else
        {
            return;
            
        }
        
    }

    public void electroTowerSelected()
    {
        TowerSelected = true;
        turretselected = ElectroTower;

        if (ghostInstance != null)
            Destroy(ghostInstance);

        ghostInstance = Instantiate(electroTowerGhost);
    }

    void Update()
    {
        if (ghostInstance != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ghostInstance.transform.position = new Vector3(mousePosition.x, mousePosition.y, 890f);


            if (ghostCollider == null)
                ghostCollider = ghostInstance.GetComponent<Collider2D>();

            // Obtener todos los colliders en el área del fantasma (aunque sean triggers)
            Collider2D[] overlaps = Physics2D.OverlapBoxAll(
                ghostCollider.bounds.center,
                ghostCollider.bounds.size,
                0f,
                Turrethitbox | Path
            );

            // Filtrar por tag si quieres (opcional):
            bool hayColision = overlaps.Length > 0;

            var sr = ghostInstance.GetComponent<SpriteRenderer>();
            sr.color = hayColision
                ? new Color(1f, 0f, 0f, 0.5f) // rojo si choca
                : new Color(0f, 1f, 0f, 0.5f); // verde si se puede colocar

            if (!hayColision && Input.GetMouseButtonDown(0))
            {
                CurrencySystem.Instance.SpendCurrency(10);
                Vector3 finalPosition = new Vector3(mousePosition.x, mousePosition.y, currentZ);
                Instantiate(turretselected, finalPosition, Quaternion.identity);
                Destroy(ghostInstance);
                ghostCollider = null;
                TowerSelected = false;
                currentZ -= zStep;
                
            }
        }
    }
}

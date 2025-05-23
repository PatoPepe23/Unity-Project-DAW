using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalPathLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LivesSystem.Instance != null)
        {
            Debug.LogError("Error: LivesSystem.Instance no está disponible. Asegúrate de que el GameObject con LivesSystem esté en la escena y activo.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyLogic enemy = collision.transform.GetComponent<EnemyLogic>();
        int damage = enemy.damage;

         LivesSystem.Instance.LoseLive(damage);
         
        Destroy(collision.gameObject);
    }
}

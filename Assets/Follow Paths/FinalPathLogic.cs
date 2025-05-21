using TMPro;
using UnityEngine;

public class FinalPathLogic : MonoBehaviour
{
    
    public int lives = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyLogic enemy = collision.transform.GetComponent<EnemyLogic>();
        int damage = enemy.damage;

        lives -= damage;
        
        GameObject.Find("LivesDisplay").GetComponent<TextMeshProUGUI>().text = lives.ToString();
        
        
        Destroy(collision.gameObject);
    }
}

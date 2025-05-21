using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int damage;
    public int health = 100;
    public Sprite[] sprites;
    
    Rigidbody2D rb;
    SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletBehavior bullet = collision.gameObject.GetComponent<BulletBehavior>();
            TakeDamage(bullet.damage);
            
            Destroy(collision.gameObject);
        }
    }

    [ContextMenu("TakeDamage")]
    void TakeDamage(int damage)
    {
        health -= damage;
        // health -= 1;

        if (health <= 0)
        {
            Destroy(gameObject);   
        }
        else if (health <= 33)
        {
            sr.sprite = sprites[2];
        } else if (health <= 67)
        {
            sr.sprite = sprites[1];
        }
    }
}

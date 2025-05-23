using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public int damage;
    public int health = 100;
    public int money;
    private Animator animator;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletBehavior bullet = collision.gameObject.GetComponent<BulletBehavior>();
            TakeDamage(bullet.damage);
            // Destroy(collision.gameObject);
        }
    }

    [ContextMenu("TakeDamage")]
    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            CurrencySystem.Instance.AddCurrency(money);
            Destroy(gameObject);
        }
        else if (health <= 25)
        {
            animator.SetTrigger("DamagedCritical");
        }
        else if (health <= 33)
        {
            animator.SetTrigger("DamagedLow");
        }
        else if (health <= 67)
        {
            animator.SetTrigger("DamagedMedium");
        }
    }
}

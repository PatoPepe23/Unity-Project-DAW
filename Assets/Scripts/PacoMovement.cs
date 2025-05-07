using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class PacoMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int cooldownTime = 1;
    public GameObject bullet;
    public Vector2 position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 direction = (target - position).normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            GameObject bulletInstance = Instantiate(bullet, position, rotation);
            
            bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * moveSpeed;
            bulletInstance.transform.Translate(direction * Time.deltaTime);
            
        }
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;

            Vector2 enemyDirection = enemy.GetComponent<Rigidbody2D>().linearVelocity.x > 0 ? Vector2.left : Vector2.right;
            
            Vector2 enemyPosition = enemy.transform.position;
            
            Vector2 target = new Vector2(enemyPosition.x, enemyPosition.y) * enemyDirection;
            Vector2 direction = (target - position).normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            GameObject bulletInstance = Instantiate(bullet, position, rotation);
            
            Debug.Log(target);
            
            bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * moveSpeed;
            bulletInstance.transform.Translate(direction * Time.deltaTime);
            
            
            
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class FireTowerLogic : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float cooldownTime =1f;
    public GameObject bullet;
    public Vector2 position;
    bool enemyInside = false;
    // private Collider2D enemy;
    private List<Collider2D> enemies = new List<Collider2D>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = new Vector2(transform.position.x, transform.position.y + 1f);
        StartCoroutine(Shoot());
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

    void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // enemyInside = true;
        // enemy = collision;
        enemies.Add(collision);
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        enemies.RemoveAt(0);
        // enemy = enemies[0] ? enemies[0] : null;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Debug.Log(enemies.Count);
            if (enemies.Count > 0)
            {
                GameObject enemy = enemies.First().gameObject;

                if (enemy.gameObject.CompareTag("Enemy"))
                {
                    Vector2 enemyDirection = enemy.GetComponent<Rigidbody2D>().linearVelocity.x > 0 ? Vector2.left : Vector2.right;
                
                    Vector2 enemyPosition = enemy.transform.position;
                
                    Vector2 target = new Vector2(enemyPosition.x, enemyPosition.y);
                    Vector2 direction = (target - position).normalized;
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
                    GameObject bulletInstance = Instantiate(bullet, position, rotation);
                
                    bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * moveSpeed;
                    bulletInstance.transform.Translate(direction * Time.deltaTime);   
                }
            }
            yield return new WaitForSeconds(cooldownTime);
        }
    }
    
}

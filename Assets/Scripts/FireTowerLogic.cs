using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FireTowerLogic : MonoBehaviour
{
    public static FireTowerLogic SelectedTurret { get; private set; }
    public event System.Action OnTurretClicked;
    public int level = 1;
    public float moveSpeed = 5f;
    public float cooldownTime =1f;
    public GameObject bullet;
    public Vector2 position;
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
            // Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            GameObject bulletInstance = Instantiate(bullet, position, quaternion.identity);
            bulletInstance.transform.right = direction;
            
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(collision);
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (enemies.Count() > 0)
        {
            enemies.RemoveAt(0);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            enemies.RemoveAll(enemyCollider => enemyCollider == null);
            if (enemies.Count > 0)
            {
                try
                {
                    GameObject enemy = enemies.First().gameObject;
                    
                    Debug.Log(enemy);
                
                    if ( enemy != null && enemy.gameObject.CompareTag("Enemy"))
                    {
                        Vector2 enemyDirection = enemy.GetComponent<Rigidbody2D>().linearVelocity.x > 0 ? Vector2.left : Vector2.right;
                
                        Vector2 enemyPosition = enemy.transform.position;
                
                        Vector2 target = new Vector2(enemyPosition.x, enemyPosition.y);
                        Vector2 direction = (target - position).normalized;
                        GameObject bulletInstance = Instantiate(bullet, position, quaternion.identity);
                        
                        float bulletVelocity = bulletInstance.GetComponent<BulletBehavior>().velocity;
                        float distance = Vector2.Distance(position, target);
                        float timeToImpact = distance / bulletVelocity;
                        
                        Vector2 futurePosition = (Vector2)target + (Vector2)direction * bulletVelocity;
                        Debug.Log(futurePosition);
                        
                        bulletInstance.transform.right = target;
                
                        bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletVelocity;
                        bulletInstance.transform.Translate(direction * Time.deltaTime);   
                    }
                }
                catch (Exception e)
                {
                    enemies.RemoveAt(0);
                }
                
                
            }
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    public void OnMouseDown()
    {
        if (SelectedTurret != null && SelectedTurret != this)
        {
            SelectedTurret.DeselectTurret();
        }
        SelectedTurret = this;
        SelectTurret();
        OnTurretClicked?.Invoke();
    }
    
    public void SelectTurret()
    {
        Debug.Log("Torreta seleccionada: " + gameObject.name);
    }
    
    public void DeselectTurret()
    {
        Debug.Log("Torreta deseleccionada: " + gameObject.name);
        SelectedTurret = null;
    }

    void OpenTurretMenu()
    {
        
    }
    
}

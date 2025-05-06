using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class PacoMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int lives = 100;
    public int cooldownTime = 1;
    public GameObject bullet;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("space"))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x + 1f, transform.position.y + 0.3f);
            Vector2 direction = (target - myPos).normalized;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
            GameObject bulletInstance = Instantiate(bullet, myPos, rotation);
            
            bulletInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * moveSpeed;
            
            // bulletInstance.transform.Translate(direction * Time.deltaTime);
            
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("space"))
        {
            
        }

        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        rb.linearVelocity = moveDirection; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lives -= 1;
            GameObject.Find("LivesDisplay").GetComponent<TextMeshProUGUI>().text = lives.ToString();
        }
    }
    
}

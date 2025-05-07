using UnityEngine;
using TMPro;

public class JoseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int lives = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        
        transform.Translate(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            lives -= 1;
            GameObject.Find("LivesDisplay").GetComponent<TextMeshProUGUI>().text = lives.ToString();
        }
    }
}

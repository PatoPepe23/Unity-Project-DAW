using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float velocity;
    public int damage;
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
        CurrencySystem.Instance.AddCurrency(5);
        Destroy(this.gameObject);
        Debug.Log(collision.gameObject.name);
    }
}

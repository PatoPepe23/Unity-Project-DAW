using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public AudioClip bulletSound;
    private AudioSource audioSource;

    public float velocity;
    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.PlayOneShot(bulletSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Your existing logic for adding currency
            // Make sure CurrencySystem.Instance exists and AddCurrency is defined.
            CurrencySystem.Instance.AddCurrency(5);
            
            Debug.Log(this.gameObject.name);
            // Check if THIS bullet's name IS NOT "ThunderBullet(Clone)"
            // Remember that Unity adds "(Clone)" to instantiated prefabs.
            if (this.gameObject.name != "ThunderBullet(Clone)")
            {
                Destroy(this.gameObject); // Destroys the bullet if it's NOT a ThunderBullet
            }

            Debug.Log("Bullet collided with: " + collision.gameObject.name);
        }
    }
}

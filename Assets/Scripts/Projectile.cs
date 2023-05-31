using UnityEngine;

/// <summary>
/// Handle the projectile launched by the player to fix the robots.
/// </summary>
public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    private bool initialized = false;
    private RingPrefeb ringPrefeb; // 保存 PanelControl 组件的引用
    private float speed = 3;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        ringPrefeb = FindObjectOfType<RingPrefeb>(); // 获取 PanelControl 组件的引用
    }

    void Update()
    {
        if (initialized) transform.Translate(speed * Time.deltaTime, 0, 0);
        //destroy the projectile when it reach a distance of 1000.0f from the origin
        if (transform.position.magnitude > 1000.0f)
            Destroy(gameObject);
    }

    //called by the player controller after it instantiate a new projectile to launch it.
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    public void Launch(Vector2 startPos, Vector2 direction, float speed, float lifeTime)
    {
        direction = direction.normalized;
        transform.position = startPos + direction;
        this.speed = speed;
        // Invoke(nameof(DestroyBall), lifeTime);
        initialized = true;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy e = other.collider.GetComponent<Enemy>();
        RubyController self = other.collider.GetComponent<RubyController>();
        if (self != null) return;
        //if the object we touched wasn't an enemy, just destroy the projectile.
        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);
    }
}

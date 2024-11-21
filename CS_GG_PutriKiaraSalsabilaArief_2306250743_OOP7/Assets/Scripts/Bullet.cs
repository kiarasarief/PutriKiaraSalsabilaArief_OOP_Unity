//Putri Kiara Salsabila Arief(2306150743)
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
        CheckCameraBounds();
    }

    private void CheckCameraBounds()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 bulletPosition = transform.position;

        float xMin = cameraPosition.x - cameraWidth / 2;
        float xMax = cameraPosition.x + cameraWidth / 2;
        float yMin = cameraPosition.y - cameraHeight / 2;
        float yMax = cameraPosition.y + cameraHeight / 2;

        if (bulletPosition.x < xMin || bulletPosition.x > xMax || bulletPosition.y < yMin || bulletPosition.y > yMax)
        {
            if (objectPool != null)
            {
                objectPool.Release(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag(gameObject.tag)) 
    {
        return;
    }

    HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
    if (hitbox != null)
    {
        Debug.Log($"{gameObject.name} collided with {other.gameObject.name} and is dealing {damage} damage.");
        hitbox.Damage(damage);  
    }

    objectPool.Release(this);
}

    public void ResetBullet()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}

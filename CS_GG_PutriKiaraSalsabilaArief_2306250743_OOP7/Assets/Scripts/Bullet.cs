using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    private float timeoutDelay = 0f;
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
        CameraBound();
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateAfterTimeout(timeoutDelay));
    }

    private IEnumerator DeactivateAfterTimeout(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector2.zero;  
        rb.angularVelocity = 0;      

        objectPool.Release(this);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            objectPool.Release(this); 
        }
    }

    private void CameraBound()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) 
        {
            return; 
        }
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
            if(objectPool != null)
            {
                objectPool.Release(this); 
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    private float speed = 1.5f;  // 3/2f
    private float xMin, xMax;

    private float CameraTop, CameraBottom, CameraRight, CameraLeft;

    [Header("Weapon System")]
    [SerializeField] private EnemyWeapon weaponPrefab;
    private EnemyWeapon equippedWeapon;

    private void Start()
    {
        CameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        CameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        CameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        CameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        // Spawn musuh secara random di kiri atau kanan layar
        Vector3 spawn;
        if (Random.value < 0.5f)
        {
            spawn = new Vector3(CameraLeft, Random.Range(CameraBottom + 3f, CameraTop), transform.position.z);
        }
        else
        {
            spawn = new Vector3(CameraRight, Random.Range(CameraBottom + 3f, CameraTop), transform.position.z);
        }

        transform.position = spawn;

        // Hitung batas-batas pergerakan
        xMin = CameraLeft;
        xMax = CameraRight;

        // Tentukan arah pergerakan
        speed *= spawn.x < 0 ? 1 : -1;

        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (weaponPrefab != null)
        {
            equippedWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
            equippedWeapon.parentTransform = transform;
        }
        else
        {
            Debug.LogWarning("WeaponPrefab not assigned to EnemyBoss!");
        }
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Jika melewati batas, balik arah
        if (pos.x < xMin || pos.x > xMax)
        {
            speed *= -1;
        }
    }
}

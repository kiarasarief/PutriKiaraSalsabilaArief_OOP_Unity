//Putri Kiara Salsabila Arief (2306250743)
using Unity.VisualScripting;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 2f;
    private float yMin, yMax;
    private float CameraTop;
    private float CameraBottom;
    private float CameraRight;
    private float CameraLeft;

    EnemyForward()
    {
        level = 1;
    }

    private void Start()
    {
        CameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        CameraBottom = Camera.main.transform.position.y -Camera.main.orthographicSize;

        CameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize;

        CameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize;
        Vector3 spawn;
        spawn = new Vector3(Random.Range(CameraLeft, CameraRight), CameraTop, transform.position.z);
        transform.position = spawn;

        yMin = CameraBottom;

        speed *= spawn.y < 0 ? 1 : -1;
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Vector3 pos = transform.position;
        pos.y += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.y < yMin-box.size.y/2)
        {
            Destroy(gameObject);
        }
    }
}
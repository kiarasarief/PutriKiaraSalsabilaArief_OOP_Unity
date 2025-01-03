//Putri Kiara Salsabila Arief (2306250743)
using System.Buffers.Text;
using UnityEngine;

public class EnemyBoss : Enemy
{
    private float speed = 3/2f;
    private float xMin, xMax;

    EnemyBoss()
    {
        level = 3;
    }

    private float CameraTop, CameraBottom, CameraRight, CameraLeft;

    private void Start()
    {
        CameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        CameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        CameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        CameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        
        Vector3 spawn;
        if(Random.value < 0.5f)
        {
            spawn = new Vector3(CameraLeft, Random.Range(CameraBottom + 3f, CameraTop), transform.position.z);
        }
        else
        {
            spawn = new Vector3(CameraRight, Random.Range(CameraBottom+3f, CameraTop), transform.position.z);
        }

        transform.position = spawn;

        xMin = CameraLeft;
        xMax = CameraRight;

        speed *= spawn.x < 0 ? 1 : -1;
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

        if (pos.x < xMin || pos.x > xMax)
        {
            speed *= -1;
        }
    }
}
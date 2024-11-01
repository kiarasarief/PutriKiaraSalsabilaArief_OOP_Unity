// Putri Kiara Salsabila Arief (2306250743)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    void Start()
    {
        // Ambil referensi dari Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Inisialisasi kecepatan awal ke nol
        moveVelocity = Vector2.zero;
        rb.velocity = Vector2.zero;

        // Kalkulasi nilai untuk moveFriction dan stopFriction sesuai rumus
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        // Mendapatkan input dari pengguna 
        moveDirection = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));


        // Menghitung moveVelocity dengan friction dan input
        moveVelocity += moveDirection * GetFriction() * Time.deltaTime;

        // Batasi kecepatan pada maxSpeed
        moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxSpeed.x, maxSpeed.x);
        moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxSpeed.y, maxSpeed.y);

        // Menerapkan kecepatan pada Rigidbody2D
        rb.velocity = moveVelocity;

        // Jika kecepatan lebih rendah dari stopClamp, set moveVelocity menjadi nol
        if (moveVelocity.magnitude < stopClamp.magnitude)
        {
            moveVelocity = Vector2.zero;
        }

        // Panggil MoveBound untuk membatasi posisi
        MoveBound();
    }

    private Vector2 GetFriction()
    {
        // Mengembalikan nilai gesekan yang sesuai
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    private void MoveBound()
    {
        Vector3 pos = transform.position;
        
        // Atur batas posisi sesuai ukuran layar (sesuaikan nilai dengan area permainan)
        pos.x = Mathf.Clamp(pos.x, -8f, 8f); // batas kiri dan kanan
        pos.y = Mathf.Clamp(pos.y, -4f, 4f); // batas atas dan bawah

        // Terapkan posisi yang sudah di-*clamp* kembali ke transform
        transform.position = pos;
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika Player bergerak, false jika tidak
        return moveVelocity.magnitude > 0;
    }
}



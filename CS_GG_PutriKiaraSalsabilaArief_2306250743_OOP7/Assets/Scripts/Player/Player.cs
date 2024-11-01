// Putri Kiara Salsabila Arief (2306250743)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // static instance untuk membuat singleton
    public static Player Instance;

    private PlayerMovement playerMovement;
    private Animator animator;

    void Awake()
    {
        // implementasi singleton pattern
        if (Instance == null)
        {
            Instance = this;  // tetapkan instance ini sebagai instance tunggal
            DontDestroyOnLoad(gameObject); // pilihan: jangan hancurkan saat berpindah scene
        }
        else
        {
            // Jika sudah ada instance lain, hancurkan objek ini
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ambil referensi dari PlayerMovement dan Animator
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("EngineEffect").GetComponent<Animator>(); // menemukan Animator di anak GameObject "EngineEffect"
    }

    void FixedUpdate()
    {
        // memanggil method Move() dari PlayerMovement untuk menggerakkan player
        playerMovement.Move();
    }

    void LateUpdate()
    {
        // mengatur nilai parameter IsMoving pada Animator berdasarkan kondisi PlayerMovement
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}


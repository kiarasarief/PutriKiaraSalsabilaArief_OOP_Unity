//Putri Kiara Salsabila Arief (2306250743)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    private PlayerMovement playerMovement;
    private Animator animator;
    public Weapon currentWeapon;


    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(gameObject); 
        } 
        else { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate() {
        playerMovement.Move();
    }

    void LateUpdate() {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
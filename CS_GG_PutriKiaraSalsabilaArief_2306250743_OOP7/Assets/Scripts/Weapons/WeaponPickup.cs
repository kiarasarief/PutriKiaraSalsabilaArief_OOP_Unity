using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake() {
        weapon = weaponHolder;
    }

    void Start() {
        if (weapon != null) {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && weapon != null) {
            Player player = other.GetComponent<Player>();

            if (player.currentWeapon != null) {
                player.currentWeapon.gameObject.SetActive(false);
            }

            Weapon instantiate_Weapon = Instantiate(weapon, other.transform);
            
            instantiate_Weapon.transform.localPosition = Vector2.zero;
            instantiate_Weapon.transform.localRotation = Quaternion.identity;

            player.currentWeapon = instantiate_Weapon;
            TurnVisual(true, instantiate_Weapon);  
        }
    }

    void TurnVisual(bool on) {
        weapon.gameObject.SetActive(on);
    }

    void TurnVisual(bool on, Weapon weapon) {
        weapon.gameObject.SetActive(on);
    }
}
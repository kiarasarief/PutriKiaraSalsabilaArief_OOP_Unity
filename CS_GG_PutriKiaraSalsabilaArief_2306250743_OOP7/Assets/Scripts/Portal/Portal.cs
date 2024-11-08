using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    private Vector2 newPosition;

    private void Start() {
        ChangePosition();
    }

    private void Update() {
        if (Player.Instance != null) {
            if (Player.Instance.currentWeapon != null) {
                GetComponent<Collider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            } else {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        } 

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, newPosition) < 0.5f) {
            ChangePosition();
        }
    }

    private void ChangePosition() {
        float posisirandom_X = Random.Range(-4, 4);
        float posisirandom_Y = Random.Range(-2, 2);
        newPosition = new Vector2(posisirandom_X, posisirandom_Y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player")) {
            if (player != null && player.currentWeapon != null) {               
                GameManager.Instance.LevelManager.LoadScene("Main");
            }
        }
    }
}
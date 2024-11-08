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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = (-2) * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = (-2) * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move() {
        float input_X = Input.GetAxis("Horizontal");
        float input_Y = Input.GetAxis("Vertical");

        if (input_X == 0 && input_Y == 0) {
            rb.velocity = Vector2.zero;
            return;
        }

        moveDirection = new Vector2(input_X, input_Y).normalized;
        Vector2 friction = GetFriction();

        Vector2 newVelocity = rb.velocity + (moveDirection * moveVelocity) - friction;
        
        rb.velocity = new Vector2(
            Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y)
        );    

        MoveBound();
    }

    public Vector2 GetFriction() {
        Vector2 friction = Vector2.zero;

        if (moveDirection == Vector2.zero) {
            friction.x = rb.velocity.x * stopFriction.x;
            friction.y = rb.velocity.y * stopFriction.y;
        }
        else {
            friction.x = rb.velocity.x * moveFriction.x;
            friction.y = rb.velocity.y * moveFriction.y;
        }

        friction.x = Mathf.Clamp(friction.x, -Mathf.Abs(rb.velocity.x), Mathf.Abs(rb.velocity.x));
        friction.y = Mathf.Clamp(friction.y, -Mathf.Abs(rb.velocity.y), Mathf.Abs(rb.velocity.y));

        return friction;
    }

    private void MoveBound() {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, min.x + (transform.localScale.x / 3), max.x - (transform.localScale.x / 3)),
            Mathf.Clamp(transform.position.y, min.y + (transform.localScale.y / 10), max.y - (transform.localScale.y / 1.5f))
        );
    }

    public bool IsMoving() {
        return (Mathf.Abs(rb.velocity.x) > stopClamp.x) || (Mathf.Abs(rb.velocity.y) > stopClamp.y);
    }
}
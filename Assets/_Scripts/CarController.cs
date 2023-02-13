using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float acceleration = 1.0f;
    public float rotationSpeed = 180.0f;
    public float speed;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        Vector2 movement = new Vector2(horizontal, vertical);

        movement = movement.normalized * acceleration * Time.deltaTime;

        rb2d.AddForce(movement, ForceMode2D.Force);

        speed = rb2d.velocity.magnitude;

        if (speed > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }

        float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}

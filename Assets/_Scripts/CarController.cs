using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private float accelerationFactor = 30f;
    [SerializeField] private float turnFactor = 3.5f;
    [SerializeField] private float driftFactor = 0.95f;
    [SerializeField] private float maxSpeed = 20;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private Menu menu;

    private float _accelerationInput = 0;
    private float _steeringInput = 0;
    private float _rotationAngle = 0;
    private float _velocityVsUp = 0;

    private Rigidbody2D _rb;
    private float delay = 1;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }

        if (_renderer.isVisible == false)
        {
            menu.Enable();
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    private void ApplySteering()
    {

        float minSpeedBeforeAllowTurningFactor = (_rb.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        _rotationAngle -= _steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        _rb.MoveRotation(_rotationAngle);
    }

    private void ApplyEngineForce()
    {
        _velocityVsUp = Vector2.Dot(transform.up, _rb.velocity);

        if (_velocityVsUp > maxSpeed && _accelerationInput > 0) return;
        if (_velocityVsUp < -maxSpeed * 0.5f && _accelerationInput < 0) return;
        if (_rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && _accelerationInput > 0) return;

        if (_accelerationInput == 0)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            _rb.drag = 0;
        }

        Vector2 engineForceVector = transform.up * _accelerationInput * accelerationFactor;

        _rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVellocity = transform.up * Vector2.Dot(_rb.velocity, transform.up);

        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb.velocity, transform.right);

        _rb.velocity = forwardVellocity + rightVelocity * driftFactor;
    }

    public void SetUpInputVector(Vector2 inputVector)
    {
        _accelerationInput = inputVector.y;
        _steeringInput = inputVector.x;
    }
}

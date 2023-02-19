using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

    private CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();
    }

    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetUpInputVector(inputVector);
    }


}

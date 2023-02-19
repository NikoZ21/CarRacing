using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = startPos;
    }
}

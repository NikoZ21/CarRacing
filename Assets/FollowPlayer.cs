using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + offset.y, target.transform.position.z + offset.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float xAmount = 0.0f;
    [SerializeField] private float yAmount = 0.0f;
    [SerializeField] private float zAmount = 0.0f;

    void Update()
    {
        transform.Rotate(new Vector3(xAmount, yAmount, zAmount) * Time.deltaTime, Space.Self);
    }
}

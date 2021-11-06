using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Stay On Screen Position")]
public class StayOnScreenPosition : MonoBehaviour
{
    Vector3 position;
    void Start()
    {
        position = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}

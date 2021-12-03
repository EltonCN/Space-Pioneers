using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("SpacePioneers/Stay On Screen Position")]
public class StayOnScreenPosition : MonoBehaviour
{
    [SerializeField] Vector3 targetScreenPosition;
    [SerializeField] Vector3 actualScreenPosition;
    [SerializeField] bool updateTarget = false;

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(targetScreenPosition);
    }

    void OnValidate()
    {
        actualScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if(updateTarget)
        {
            targetScreenPosition = actualScreenPosition;
            updateTarget = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ReflectPoint : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform firstControlPoint;
    [SerializeField] private Transform secondControlPoint;

    private void Update()
    {
        Vector3.Distance(centerPoint.position, firstControlPoint.position);
        secondControlPoint.position = centerPoint.position + ((firstControlPoint.position - centerPoint.position) * -1);
    }
}

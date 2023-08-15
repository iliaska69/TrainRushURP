using System.Runtime.InteropServices;
using UnityEngine;

public class ParkingPlace : MonoBehaviour
{
    [SerializeField] private Parking parking;
    [SerializeField] private BezierCurve curve;

    public BezierCurve GetCurve()
    {
        return curve;
    }
}

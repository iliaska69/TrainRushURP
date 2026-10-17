using UnityEngine;

public struct CameraSettings
{
    public Transform CameraTransform;
    public Quaternion CameraRotation;
    public bool IsCameraPositionFixed;
    public bool IsCameraRotationFixed;
    public Vector3 CameraMoveDirection;

    public CameraSettings(
        Transform cameraTransform,
        Quaternion cameraRotation,
        bool isCameraPositionFixed,
        bool isCameraRotationFixed,
        Vector3 cameraMoveDirection)
    {
        CameraTransform = cameraTransform;
        CameraRotation = cameraRotation;
        IsCameraPositionFixed = isCameraPositionFixed;
        IsCameraRotationFixed = isCameraRotationFixed;
        CameraMoveDirection = cameraMoveDirection;
    }
}
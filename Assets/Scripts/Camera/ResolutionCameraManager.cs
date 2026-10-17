using UnityEngine;

public class ResolutionCameraManager : MonoBehaviour
{
    [SerializeField] private Camera verticalCamera;
    [SerializeField] private Camera horizontalCamera;
    
    void Start()
    {
        var isHorizontal = UIController.instance.IsHorizontal();
        if (isHorizontal)
        {
            horizontalCamera.gameObject.SetActive(true);
            verticalCamera.gameObject.SetActive(false);
        }
        else
        {
            verticalCamera.gameObject.SetActive(true);
            horizontalCamera.gameObject.SetActive(false);
        }
    }
}

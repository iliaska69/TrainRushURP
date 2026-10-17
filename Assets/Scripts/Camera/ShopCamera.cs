using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShopCamera : MonoBehaviour
{
    [SerializeField] private UniversalAdditionalCameraData universalAdditionalMainCameraData;
    private UIController _uiController;
    
    private void Start()
    {
        _uiController = UIController.instance;
        WaitForOverlayCameraAsync().Forget();
    }
    
    async UniTaskVoid WaitForOverlayCameraAsync()
    {
        Camera uiCamera = null;
        while (uiCamera == null)
        {
            uiCamera = _uiController.GetUICamera();
            await UniTask.Yield();
        }

        universalAdditionalMainCameraData.cameraStack.Add(uiCamera);
    }
}

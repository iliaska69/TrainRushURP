using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // private Camera _camera;
    //
    // void Start()
    // {
    //     _camera = Camera.main;
    // }
    //
    // void Update()
    // {
    //     ButtonClick();
    // }
    //
    // public void ButtonClick()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         var ray = _camera.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;
    //
    //         if (Physics.Raycast(ray, out hit, 100))
    //         {
    //             var button = hit.transform.gameObject.GetComponent<IClickable>();
    //             button?.Click();
    //         }
    //     }
    // }
}

using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class BranchingRail : Rail
{
    [SerializeField] private List<GameObject> branches;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        ButtonClick();
    }

    public void ButtonClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Detect button
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        var train = other.gameObject.GetComponent<Train>();
        if(train is not null)
        {
            SetActiveButtons();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        var train = other.gameObject.GetComponent<Train>();
        if (train is not null)
        {
            SetInactiveButtons();
        }
    }

    private void SetActiveButtons()
    {
        rightButton.SetActive(true);
        leftButton.SetActive(true);
    }

    private void SetInactiveButtons()
    {
        rightButton.SetActive(false);
        leftButton.SetActive(false);
    }
}

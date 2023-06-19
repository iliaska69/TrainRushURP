using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private View startView;
    [SerializeField] private GameObject sliceWraper;

    public static UIController instance = null;

    private List<View> _views = new List<View>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (startView == null) return;

        OpenView(startView);
    }

    public void OpenView(View view)
    {
        _views.Add(view);
        CloseView();

        var slices = view.GetAllSlices();
        foreach (var slice in slices)
        {
            Instantiate(slice, sliceWraper.transform);
        }
    }

    public void CloseView()
    {
        var childCount = sliceWraper.transform.childCount;
        for(var i = 0; i<childCount; i++)
        {
            Destroy(sliceWraper.transform.GetChild(i).gameObject);
        }
    }

    public void OpenPreviousView()
    {
        OpenView(_views[_views.Count - 1]);
    }
}

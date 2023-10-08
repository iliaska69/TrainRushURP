using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private View startView;
    [SerializeField] private View emptyView;
    [SerializeField] private GameObject sliceWraper;

    public static UIController instance = null;

    private List<View> _views = new List<View>();
    private int _resolutionHeight;
    private int _resolutionWight;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
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
        TrackViewOrientation(view);
        var slices = view.GetAllSlices();
        foreach (var slice in slices)
        {
            Instantiate(slice, sliceWraper.transform);
        }
    }

    public void CloseView()
    {
        var childCount = sliceWraper.transform.childCount;
        for (var i = 0; i < childCount; i++)
        {
            Destroy(sliceWraper.transform.GetChild(i).gameObject);
        }
        if (_views.Count > 3)
        {
            _views.Remove(_views[0]);
            _views.Remove(_views[1]);
        }
    }

    public void OpenPreviousView()
    {
        if (_views.Count < 2)
        {
            OpenView(_views[_views.Count - 1]);
        }
        else
        {
            var prevView = _views[_views.Count - 2];
            _views.RemoveAt(_views.Count - 1);
            _views.RemoveAt(_views.Count - 1);
            OpenView(prevView);
        }
    }

    public void OpenEmptyView()
    {
        _views.Add(emptyView);
        CloseView();
        var slices = emptyView.GetAllSlices();
        foreach (var slice in slices)
        {
            Instantiate(slice, sliceWraper.transform);
        }
    }

    private void TrackViewOrientation(View view)
    {
        if (Screen.height != _resolutionHeight || Screen.width != _resolutionWight)
        {
            _resolutionHeight = Screen.height;
            _resolutionWight = Screen.width;
        }


        if (_resolutionHeight > _resolutionWight)
        {
            view.isHorizontal = false;
        }
        else
        {
            view.isHorizontal = true;
        }
    }
}

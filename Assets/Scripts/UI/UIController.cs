using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] private View startLoadingView;
    [SerializeField] private View emptyView;
    [SerializeField] private GameObject sliceWraper;

    [SerializeField] private EventSystem eventSystem;

    public static UIController instance;

    private List<View> _views = new();
    private int _resolutionHeight;
    private int _resolutionWight;
    private bool _viewIsClose;
    private int _countOfClosedSlices;
    private List<Slice> _newSlices = new();
    private bool _isFirstEntry = true;
    private int _countOfSlices;
    private bool _isHorizontal;

    
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
        if (startLoadingView == null) return;

        OpenView(startLoadingView);
    }

    public void OpenView(View view)
    {
        DisableUIInteraction();
        view.isHorizontal = false;
        
        if (_isFirstEntry)
        {
            EnableUIInteraction();
            _isFirstEntry = false;
        }

        CloseView();
        _views.Add(view);

        TrackViewOrientation(view);
        
        _newSlices = view.GetAllSlices();
        foreach (var slice in _newSlices)
        {
            var newSlice = Instantiate(slice, sliceWraper.transform);
            newSlice.SetUICamera(uiCamera);
        }
    }

    public void CloseView()
    {
        var childCount = sliceWraper.transform.childCount;
        _countOfSlices = childCount;
        for (var i = 0; i < childCount; i++)
        {
            StartCoroutine(CloseSliceAnimations(sliceWraper.transform.GetChild(i).gameObject));
        }

        if (_views.Count > 3)
        {
            _views.Remove(_views[0]);
            _views.Remove(_views[1]);
        }
    }

    public Camera GetUICamera() => uiCamera;

    public void EnableUIInteraction()
    {
        if (eventSystem == null) return;

        eventSystem.gameObject.SetActive(true);
    }

    public void DisableUIInteraction()
    {
        if (eventSystem == null) return;

        eventSystem.gameObject.SetActive(false);
    }
    
    IEnumerator CloseSliceAnimations(GameObject slice)
    {
        //убирает баг закрытия анимации
        //yield return new WaitForSecondsRealtime(0.1f);

        if (slice == null)
            yield return null;

        var currentSlice = slice.GetComponent<Slice>();

        if (currentSlice == null)
            yield return null;

        var duration = currentSlice.GetCloseAnimationDuration();
        currentSlice.PlayCloseAnimations();

        yield return new WaitForSecondsRealtime(duration);
        
        if (currentSlice != null)
        {
            CheckFinishCoroutine();
            Destroy(currentSlice.gameObject);
        }
    }

    private void CheckFinishCoroutine()
    {
        _countOfClosedSlices++;
        if (_countOfClosedSlices != _countOfSlices) return;
       
        EnableUIInteraction();
        _countOfClosedSlices = 0;
        _countOfSlices = 0;
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
            _isHorizontal = false;
        }
        else
        {
            view.isHorizontal = true;
            _isHorizontal = true;
        }
    }

    public bool IsHorizontal()
    {
        return _isHorizontal;
    }
}
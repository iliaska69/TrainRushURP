using UnityEngine;

public class UINavigator : MonoBehaviour
{
    [SerializeField] private View view;
    [SerializeField] private bool openPrevious;


    public void OpenView()
    {
        if (openPrevious)
        {
            UIController.instance.OpenPreviousView();
        }
        else
        {
            UIController.instance.OpenView(view);
        }
            
    }
}

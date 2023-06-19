using UnityEngine;

public class UINavigator : MonoBehaviour
{
    [SerializeField] private View view;


    public void OpenView(View view)
    {
        UIController.instance.OpenView(view);
    }

    public void OpenPreviousView()
    {
        UIController.instance.OpenPreviousView();
    }
}

using TMPro;
using UnityEngine;

public class AdminLevelChoose : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    
    public void Console()
    {
        LevelManager.instance.LoadLevelAdmin(inputField.text);
    }
}

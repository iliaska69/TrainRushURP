using TMPro;
using UnityEngine;

public class UIMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

    private void Start()
    {
        money.text = PlayerPrefs.GetInt("Balance").ToString();
    }

    public void SetMoney(int value)
    {
        money.text = value.ToString();
    }
}

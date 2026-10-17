using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenVerticalScaler : MonoBehaviour
{
    public Sprite spriteTall;      // 1080x2400 (например, для 20:9)
    public Sprite spriteStandard;  // 1080x1920 (например, для 16:9)
    public Sprite spriteSquare;    // что-то более квадратное, например 1080x1080
    public Image image;
    
    private void Start()
    {
        SetResponsiveImage();
    }

    void SetResponsiveImage()
    {
        float aspect = (float)Screen.width / Screen.height;

        if (aspect < 0.5f) // очень высокий экран, например 20:9
        {
            Debug.Log("Tall");
            image.sprite = spriteTall;
        }
        else if (aspect < 0.65f) // стандартный телефон, например 16:9
        {
            Debug.Log("defaulty");
            image.sprite = spriteStandard;
        }
        else
        {
            Debug.Log("square");
            image.sprite = spriteSquare;
        }
    }
}

using System.IO;
using UnityEngine;

public class screenshotmanager : MonoBehaviour
{
    private int screenshotCount = 0;
    private string folderPath;

    void Start()
    {
        // Папка для скриншотов внутри папки игры
        folderPath = Path.Combine(Application.dataPath, "Screenshots");

        // Создаём папку, если её нет
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))  // Нажми P, чтобы сделать скриншот
        {
            TakeScreenshot();
        }
    }

    void TakeScreenshot()
    {
        screenshotCount++;
        string filename = Path.Combine(folderPath, $"screenshot_{screenshotCount}.png");
        ScreenCapture.CaptureScreenshot(filename);
        Debug.Log($"Скриншот сохранён: {filename}");
    }
}

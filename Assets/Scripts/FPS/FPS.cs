using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI fpsText1;
    private float deltaTime;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Resolution[] allResolutions = Screen.resolutions;
        int maxRefresh = 30;
        for (int i = 0; i < allResolutions.Length; i++) {
            maxRefresh = Mathf.Max(maxRefresh, allResolutions[i].refreshRate);
        }
        maxRefresh = Mathf.Clamp(maxRefresh, 30, 300);
        Debug.Log(maxRefresh);
        Application.targetFrameRate = 60;
        fpsText1.text = maxRefresh.ToString();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }


}

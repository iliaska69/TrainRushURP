using UnityEngine;

public class OrientationLocker : MonoBehaviour
{
    private void Awake()
    {
        LockCurrentOrientation();
    }

    private void LockCurrentOrientation()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        Screen.orientation = Screen.width <= Screen.height
            ? ScreenOrientation.Portrait 
            : ScreenOrientation.LandscapeLeft;
    }
}

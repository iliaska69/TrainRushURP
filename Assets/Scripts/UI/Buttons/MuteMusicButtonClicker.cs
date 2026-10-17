using UnityEngine;
using UnityEngine.UI;

public class MuteMusicButtonClicker : MonoBehaviour
{
    [SerializeField] private Image unmuteImage;
    [SerializeField] private Image muteImage;
    
    private bool _isMute;

    private void Start()
    {
        UpdateButtonIcon();
    }

    public void ButtonClick()
    {
        if (_isMute)
            AudioManager.Instance.UnmuteMusic(AudioEnum.Main_Theme);
        else
            AudioManager.Instance.MuteMusic();

        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        _isMute = AudioManager.Instance.IsMusicMuted;
        if (_isMute)
        {
            unmuteImage.gameObject.SetActive(false);
            muteImage.gameObject.SetActive(true);
        }
        else
        {
            unmuteImage.gameObject.SetActive(true);
            muteImage.gameObject.SetActive(false);
        }
    }
}

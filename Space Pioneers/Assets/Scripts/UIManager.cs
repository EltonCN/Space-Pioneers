using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button playPauseButton;
    public Sprite playButtonSprite;
    public Sprite pauseButtonSprite;
    public Image actionBar;
    public void PlayPauseButtonChange()
    {
        if (playPauseButton.image.sprite == playButtonSprite) {
            playPauseButton.image.sprite = pauseButtonSprite;
        } else if (playPauseButton.image.sprite == pauseButtonSprite && actionBar.fillAmount == 0){
            playPauseButton.image.sprite = playButtonSprite;
        }
    }
}

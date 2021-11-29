using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button playPauseButton;
    public Sprite playButtonSprite;
    public Sprite pauseButtonSprite;
    public void PlayPauseButtonChange()
    {
        if (playPauseButton.image.sprite == playButtonSprite) {
            playPauseButton.image.sprite = pauseButtonSprite;
        } else {
            playPauseButton.image.sprite = playButtonSprite;
        }
    }
}

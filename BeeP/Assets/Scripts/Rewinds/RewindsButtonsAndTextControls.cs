using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewindsButtonsAndTextControls : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SpaceRewText = null;
    [SerializeField] private TextMeshProUGUI TimeRewText = null;
    [SerializeField] private Image SpaceRewButtonImage = null;
    [SerializeField] private Image TimeRewButtonImage = null;

    [SerializeField] private Sprite NormalButtonSprite = null;
    [SerializeField] private Sprite PressedButtonSprite = null;

    private bool _spaceButtonActiveness;
    private bool _timeButtonActiveness;

    public void UpdateActiveness(bool HasSpaceRews, bool hasTimeRews)
    {
        _spaceButtonActiveness = HasSpaceRews;
        _timeButtonActiveness = hasTimeRews;
        ToNormalTexts();
    }

    public void SpaceRewindStarted()
    {
        //SpaceRewButtonImage.color = PressedButtonColor;
        SpaceRewButtonImage.sprite = PressedButtonSprite;
        TimeRewButtonImage.sprite = NormalButtonSprite;

        SpaceRewText.text = " - release to rewind";
        TimeRewText.text = " - press to cancel";
    }

    public void TimeRewindStarted()
    {
        TimeRewButtonImage.sprite = PressedButtonSprite;
        SpaceRewButtonImage.sprite = NormalButtonSprite;

        SpaceRewText.text = " - press to cancel";
        TimeRewText.text = " - release to rewind";
    }

    public void ToNormalTexts()
    {
        SpaceRewButtonImage.sprite = _spaceButtonActiveness ? NormalButtonSprite : PressedButtonSprite;
        SpaceRewText.text = " - self rewind";
        TimeRewButtonImage.sprite = _timeButtonActiveness ? NormalButtonSprite : PressedButtonSprite;
        TimeRewText.text = " - level rewind";
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsMuteManagement : MonoBehaviour
{
    [SerializeField] private Image MainMenuSoundButton = null;
    [SerializeField] private Image PauseSoundButton = null;
    [SerializeField] private Image RestartSoundButton = null;
    //Иконки иконочки
    [SerializeField] private Sprite VolumeOffSprite = null;
    [SerializeField] private Sprite VolumeOnSprite = null;

    //Ссылка на аудиоменеджер, чтобы тогглить мут
    private AudioManager _audioManager;
    //Туда же для удобства
    private bool _soundMuted = false;

    private void Start()
    {
        //Забрали менеджер
        _audioManager = GetComponent<AudioManager>();
        if (PlayerPrefs.HasKey("Muted"))
        {
            //Если мут был установлен:
            if (PlayerPrefs.GetInt("Muted") == 1)
            {
                _soundMuted = false;
                //StartCoroutine(WaitAndMute());
                ToggleMute();
            }
        }
    }

    private void OnApplicationQuit()
    {
        //Сохраняем настроечки
        PlayerPrefs.Save();
    }

    public void ToggleMute()
    {
        //Меняем значение и тексты соответственно
        _soundMuted = !_soundMuted;
        if (_soundMuted)
        {
            PlayerPrefs.SetInt("Muted", 1);
            MainMenuSoundButton.sprite = PauseSoundButton.sprite = RestartSoundButton.sprite = VolumeOnSprite;
        }
        else
        {
            MainMenuSoundButton.sprite = PauseSoundButton.sprite = RestartSoundButton.sprite = VolumeOffSprite;
            PlayerPrefs.SetInt("Muted", 0);
        }
        _audioManager.ToggleMuteAll();
    }
}

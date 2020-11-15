using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] Buttons = null;
    private int _maxLevelReached = 1;

    private void Start()
    {
        LoadGalleryData();
    }

    private void OnEnable()
    {
        UpdateButtons();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void UpdateButtons()
    {
        _maxLevelReached = LevelUnlockedContainer.Instance.MaxLevelAchieved;
        for (int i = 0; i < _maxLevelReached && i < 15; i++)
        {
            Buttons[i].interactable = true;
            Buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Level " + (i + 1);
        }
        for (int i = _maxLevelReached; i < Buttons.Length; ++i)
        {
            Buttons[i].interactable = false;
            Buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Locked!";
        }
    }

    public void SaveData()
    {
        _maxLevelReached = LevelUnlockedContainer.Instance.MaxLevelAchieved;
        string filePath = Application.persistentDataPath + "/mld.cap";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenWrite(filePath);
        }
        else
            file = File.Create(filePath);
        BinaryWriter binaryWriter = new BinaryWriter(file);
        binaryWriter.Write(_maxLevelReached);
        binaryWriter.Close();
        file.Close();
    }

    private void LoadGalleryData()
    {

        string filePath = Application.persistentDataPath + "/mld.cap";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenRead(filePath);
            BinaryReader binaryReader = new BinaryReader(file);
            _maxLevelReached = binaryReader.ReadInt32();
            binaryReader.Close();
            file.Close();
        }
        else
        {
            _maxLevelReached = 1;
        }

        if (_maxLevelReached <= LevelUnlockedContainer.Instance.MaxLevelAchieved)
        {
            _maxLevelReached = LevelUnlockedContainer.Instance.MaxLevelAchieved;
        }
        else
            LevelUnlockedContainer.Instance.MaxLevelAchieved = _maxLevelReached;
    }
}
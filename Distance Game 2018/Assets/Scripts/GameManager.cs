using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int money;
    public float distance;
    public int gravityUpgrade;
    public int slowDownUpgrade;
    public int moveSpeedUpgrade;
    public int bounceUpgrade;
    public int speedUpUpgrade;
    public bool seenTutorial;
    public bool gamePaused;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
            PlayerPrefs.Save();
        }

        if (!File.Exists(Application.persistentDataPath + "/state.save"))
        {
            SaveGame();
        }

        distance = LoadDistance();

        LoadGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadEnding()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void SaveDistance(float dist)
    {
        distance = dist;
        if(!PlayerPrefs.HasKey("distance"))
        {
            PlayerPrefs.SetFloat("distance", 0.0f);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetFloat("distance") < distance)
        {
            PlayerPrefs.SetFloat("distance", distance);
            PlayerPrefs.Save();
        }
    }

    public float LoadDistance()
    {
        if(PlayerPrefs.HasKey("distance"))
        {
            return PlayerPrefs.GetFloat("distance");
        }

        return 0.0f;
    }

    public void ToggleSound(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
        }
        PlayerPrefs.Save();
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/state.save");
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + "/state.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/state.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            money = save.Money;
            gravityUpgrade = save.GravityUpgrade;
            slowDownUpgrade = save.SlowDownUpgrade;
            moveSpeedUpgrade = save.MoveSpeedUpgrade;
            bounceUpgrade = save.BounceUpgrade;
            speedUpUpgrade = save.SpeedUpUpgrade;
            seenTutorial = save.SeenTutorial;
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save { Money = money,
                               GravityUpgrade = gravityUpgrade,
                               SlowDownUpgrade = slowDownUpgrade,
                               MoveSpeedUpgrade = moveSpeedUpgrade,
                               BounceUpgrade = bounceUpgrade,
                               SpeedUpUpgrade = speedUpUpgrade,
                               SeenTutorial = seenTutorial };

        return save;
    }
}

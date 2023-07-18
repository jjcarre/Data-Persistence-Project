using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static ScoreManager Instance;

    public string currentName;
    public int HighScore;
    public string HighScoreName;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.name = HighScoreName;
        data.score = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(text);

            HighScore = data.score;
            HighScoreName = data.name;
        }
    }


    public void UpdateHighScore(int newScore)
    {
        Instance.HighScore = newScore;
        Instance.HighScoreName = Instance.currentName;

        SaveHighScore();
    }
}

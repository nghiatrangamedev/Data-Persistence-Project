using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Tạo ra 1 lớp thành viên là Instance. Để các Script khác có thể truy cập và sử dụng các dữ liệu và method từ biến thành viên này
    public static SaveData Instance;
    // Tạo ra 1 biến để lưu tên người chơi
    public string playerName;
    // Tạo ra 1 biến để lưu tên người chơi tốt nhất ( người chơi đạt được high score)
    public string bestPlayer;
    // Tạo ra 1 biến để lưu best score
    public float bestScore;

    // Method Awake sẽ chạy ngay khi GameObject SaveData được tạo ra
    private void Awake()
    {
        // Gán giá trị cho biên Instance bằng Script SaveData hiện tại.
        // Instance lúc này chính là SaveData.cs
        Instance = this;
        // DontDestroyOnLoad để gameObject SaveData không bị Destroy khi chuyển sang Scene khác.
        DontDestroyOnLoad(gameObject);
    }

    // Lưu tên người chơi 
    public void SaveName(string name)
    {
        playerName = name;
    }

    [System.Serializable]
    class HighScore
    {
        public string name;
        public float score;
    }

    // Lưu high score của người chơi
    public void SaveHighScore(string name, float score)
    {
        HighScore highScore = new HighScore();
        highScore.name = name;
        highScore.score = score;

        string json = JsonUtility.ToJson(highScore);

        File.WriteAllText(Application.persistentDataPath + "savehighscore.json", json);
    }

    // Load high score của người chơi
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "savehighscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore highScore = JsonUtility.FromJson<HighScore>(json);

            bestPlayer = highScore.name;
            bestScore = highScore.score;
        }
    }
}

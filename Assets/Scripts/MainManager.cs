using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    // Tạo ra 1 biến để lưu High Score
    public float highScore = 0;

    // Tạo ra 1 biến để nhận Component Text từ GameObject ScoreText
    [SerializeField] Text highScoreText;
    private string bestPlayer;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    // Hiển thị tên người chơi ngay khi trò chơi bắt đầu
    private void Awake()
    {
        //DisplayName();
        DisplayHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        // Nếu điểm hiện tại lớn hơn high score thì lưu high score lại
        // Sau khi người chơi bắt đầu lại thì sẽ hiển thị high score mới
        if (m_Points > highScore)
        {
            bestPlayer = SaveData.Instance.playerName;
            // Lưu high score ra 1 game object nằm trong DontDestroyOnLoad để giữ liệu không bị mất
            SaveData.Instance.SaveHighScore(bestPlayer,m_Points);
        }
    }

    // Hiển thị tên mà người chơi đã nhập vào từ Menu Scene
    /* public void DisplayName()
    {
        // Nhận tên người chơi đã nhập từ Menu Scene
        playerName = SaveData.Instance.LoadName();
        // Kiểm tra xem người chơi có nhập tên hay không
        // Nếu có thì sẽ hiển thị tên người chơi lên trên màn hình
        if (playerName != null)
        {
            highScoreText.text = "Score: " + playerName + " : 0 ";
        }
        // Nếu không có thì sẽ không làm gì cả
    }
    */
    public void DisplayHighScore()
    {
        // Load high score được lưu từ trước vào high score hiện tại để hiển thị lên UI
        SaveData.Instance.LoadHighScore();
        highScore = SaveData.Instance.bestScore;
        bestPlayer = SaveData.Instance.bestPlayer;
        highScoreText.text = "Score: " + bestPlayer + " : " + highScore;
    }

}

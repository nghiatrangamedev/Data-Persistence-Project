using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Import thư viện SceneManagement để sử dụng các class liên quan đến Scene
using UnityEngine.SceneManagement;
// Import thử viện Unity Editor để sử dụng các class liên quan đến Unity Editor.
// Cụ thể là Class EditorApplication và method ExitPlayMode() của nó
using UnityEditor;
// Import thư việc TMPro để làm việc với TextMeshProUI
using TMPro;

public class MenuUiHandle : MonoBehaviour
{
    // Tạo ra 1 biến để nhận giá trị từ Name Input Field
    [SerializeField] TextMeshProUGUI playerNameText;
    // Tạo ra 1 biến đên nhận giá trị từ Best Score Text
    [SerializeField] TextMeshProUGUI bestScoreText;
    // Tạo ra 2 biến để lưu tên người chơi tốt nhất và điểm của họ
    private string bestPlayer = "";
    private float bestScore = 0;

    private void Awake()
    {
        DisplayHighScore(); 
    }

    // Tạo ra GameStart() để Button Start gọi. Khi Button Start được ấn nó sẽ gọi GameStart() và chuyển sang Main Scene
    public void GameStart()
    {
        // Chuyển sang Main Scene khi người dùng bấm nút start
        // Sử dụng LoadScene() để load sang main scene
        // Vì main scene thuộc index 1 trong Build Setting nên tham số bên trong LoadScene () sẽ là 1
        SceneManager.LoadScene(1);
        // Lưu tên người chơi vào game Object SaveData thông qua SaveName()
        SaveData.Instance.SaveName(playerNameText.text);
    }

    // Tạo ra Exit() để Button Quit gọi. Khi Button Quit được ấn nó sẽ gọi Exit() để thoát khỏi PlayMode
    public void Exit()
    {
        // EditorApplication.ExitPlayMode() là câu lệnh dùng để thoat khỏi PlayMode() trong Unity Editor
        EditorApplication.ExitPlaymode();
    }

    public void DisplayHighScore()
    {
        // Hiển thị high score từ file có sẵn
        SaveData.Instance.LoadHighScore();
        bestPlayer = SaveData.Instance.bestPlayer;
        bestScore = SaveData.Instance.bestScore;

        bestScoreText.text = "Best Score : " + bestPlayer + ": " + bestScore;
    }
}

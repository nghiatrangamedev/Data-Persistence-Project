using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Import thư viện SceneManagement để sử dụng các class liên quan đến Scene
using UnityEngine.SceneManagement;
// Import thử viện Unity Editor để sử dụng các class liên quan đến Unity Editor.
// Cụ thể là Class EditorApplication và method ExitPlayMode() của nó
using UnityEditor;

public class MenuUiHandle : MonoBehaviour
{
    // Tạo ra GameStart() để Button Start gọi. Khi Button Start được ấn nó sẽ gọi GameStart() và chuyển sang Main Scene
    public void GameStart()
    {
        // Chuyển sang Main Scene khi người dùng bấm nút start
        // Sử dụng LoadScene() để load sang main scene
        // Vì main scene thuộc index 1 trong Build Setting nên tham số bên trong LoadScene () sẽ là 1
        SceneManager.LoadScene(1);
    }

    // Tạo ra Exit() để Button Quit gọi. Khi Button Quit được ấn nó sẽ gọi Exit() để thoát khỏi PlayMode
    public void Exit()
    {
        // EditorApplication.ExitPlayMode() là câu lệnh dùng để thoat khỏi PlayMode() trong Unity Editor
        EditorApplication.ExitPlaymode();
    }
}

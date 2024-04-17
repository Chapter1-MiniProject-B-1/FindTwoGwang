using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // 게임 재시작 메서드
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}

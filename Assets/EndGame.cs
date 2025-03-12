using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static bool GameEnded = false;
    public static bool GameStarted = false;
    public GameObject gameOverUI;
    public GameObject gamestartedUI;


    private void Start()
    {
        GameEnded = false;
    }
    void Update()
    {
        if (GameEnded == true)
            return;

        if (PlayerScript.Lives <= 0)
            GameOver();
    }

    void GameOver()
    {
        GameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void GameStart()
    {
        GameStarted = true;
        gamestartedUI.SetActive(false);
    }


}

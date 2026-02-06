using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public static ResultUI Instance { get; private set; }
    [SerializeField] Button mainButton;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject gameWin;
    [SerializeField] GameObject gameLose;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        Debug.Log("Return main menu");
    }

    public void Quit()
    {
        Debug.Log("You quit");
        Application.Quit();
    }

    public void GameLose()
    {
        gameLose.SetActive(true);
    }
}

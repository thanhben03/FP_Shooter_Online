using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviourPun
{
    public static ResultUI Instance { get; private set; }
    [SerializeField] Button mainButton;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject gameWin;
    [SerializeField] GameObject gameLose;

    private void Awake()
    {
        Instance = this;
        mainButton.onClick.AddListener(() =>
        {
            MainMenu();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
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
    public void GameWin()
    {
        gameWin.SetActive(true);
    }
}

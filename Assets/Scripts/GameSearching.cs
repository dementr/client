using System.Collections;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSearching : MonoBehaviour
{
    SocketIOComponent socket;

    public TMPro.TMP_Text timerText;

    public GameObject gameFoundWindow;

    Coroutine timerRoutine;
    WaitForSeconds delay = new WaitForSeconds(1);

    void Awake()
    {
        socket = NetworkMain.inst.socket;
    }

    #region Search game

    public void StartSearh()
    {
        socket.Emit("searchGame");
        StartTimer();
    }

    public void StopSearch()
    {
        socket.Emit("stopSearch");
        StopTimer();
    }

    IEnumerator TimerCoroutine()
    {
        timerText.text = SerchTimer.DefaultState();

        while (true)
        {
            yield return delay;
            SerchTimer.Searching();
            timerText.text = SerchTimer.Time();
        }
    }

    void StartTimer()
    {
        timerRoutine = StartCoroutine(TimerCoroutine());
    }

    void StopTimer()
    {
        StopCoroutine(timerRoutine);
    }

    #endregion

    public void AcceptGame()
    {
        socket.Emit("acceptGame");
    }

    public void CancelGame()
    {
        socket.Emit("cancelGame");
    }

    void OnGameFound(SocketIOEvent e)
    {
        gameFoundWindow.SetActive(true);
    }

    void OnGameReady(SocketIOEvent e)
    {
        SceneManager.LoadScene("Game");
    }

    void OnAnotherPlayerCancelGame(SocketIOEvent e)
    {
        MessageWindow.inst.ShowError("Another player cancel the game");
        gameFoundWindow.SetActive(false);
    }

    void OnEnable()
    {
        socket.On("gameFound", OnGameFound);
        socket.On("gameReay", OnGameReady);
        socket.On("cancelGame", OnAnotherPlayerCancelGame);
    }

    void OnDisable()
    {
        socket.Off("gameReady", OnGameFound);
        socket.Off("gameReay", OnGameReady);
        socket.Off("cancelGame", OnAnotherPlayerCancelGame);
    }
}

using System.Collections;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSearching : MonoBehaviour
{
    SocketIOComponent socket;

    public TMPro.TMP_Text timerText;

    public GameObject startBtn, stopBtn, gameFoundWindow, buttons, acceptedText;

    Coroutine timerRoutine;
    WaitForSeconds delay = new WaitForSeconds(1);

    #region Game searching

    public void StartSearh()
    {
        string json = JsonUtility.ToJson(new GameSearchingInfo("start"));
        socket.Emit("searchGame", new JSONObject(json));
    }

    public void StopSearch()
    {
        string json = JsonUtility.ToJson(new GameSearchingInfo("stop"));
        socket.Emit("searchGame", new JSONObject(json));
    }

    public void AcceptGame()
    {
        OnGameAccepted();

        GameSearchingInfo info = new GameSearchingInfo("accepted");

        string json = JsonUtility.ToJson(info);
        socket.Emit("searchGame", new JSONObject(json));
    }

    public void CancelGame()
    {
        GameSearchingInfo info = new GameSearchingInfo("cancel");

        string json = JsonUtility.ToJson(info);
        socket.Emit("searchGame", new JSONObject(json));
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
        StopTimer();
        timerRoutine = StartCoroutine(TimerCoroutine());
    }

    void StopTimer()
    {
        StopCoroutine(timerRoutine);
    }

    #endregion

    void OnGameFound()
    {
        gameFoundWindow.SetActive(true);
        StartTimer();
    }

    void OnGameAccepted()
    {
        acceptedText.SetActive(true);
        buttons.SetActive(false);
    }

    void OnGameCanceled()
    {
        startBtn.SetActive(true);
        stopBtn.SetActive(false);
        gameFoundWindow.SetActive(false);
        acceptedText.SetActive(false);
        buttons.SetActive(true);

        MessageWindow.inst.ShowError("Game canceled");
    }

    void OnSearchGame(SocketIOEvent e)
    {
        GameSearchingInfo info = GameSearchingInfo.FromJson(e.data.ToString());

        switch (info.status)
        {
            case "added": StartTimer(); break;
            case "removed": StopTimer(); break;
            case "found": OnGameFound(); break;
            case "canceled": OnGameCanceled(); break;

            case "failure": MessageWindow.inst.ShowError("Error"); break;
            case "css": SceneManager.LoadScene("Game"); break;
        }
    }

    void OnEnable()
    {
        socket = NetworkMain.inst.socket;

        socket.On("searchGame", OnSearchGame);
    }

    void OnDisable()
    {
        socket.Off("searchGame", OnSearchGame);
    }
}

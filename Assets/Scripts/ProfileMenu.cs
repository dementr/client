﻿using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
{
    SocketIOComponent socket;

    public GameObject chatPanel, activateChatbutton;

    void Start()
    {
        socket = NetworkMain.inst.socket;

        Init();
    }

    void Init()
    {
        socket.On("joinr", JoinRoom);
        socket.On("joiGame", JoinGame);
    }

    public void JoinChat()
    {
        string nickJson = "{ \"nick\": \"" + NetworkMain.inst.nickname + "\" }";
        socket.Emit("joinchat", new JSONObject(nickJson));

        chatPanel.SetActive(true);
        activateChatbutton.SetActive(false);
    }

    public void StartSearh()
    {
        socket.Emit("searchGame");
    }

    public void JoinGame(SocketIOEvent e)
    {
        Debug.Log(e.data.ToString());
        SceneManager.LoadScene("Game");
    }

    public void JoinRoom(SocketIOEvent e)
    {
        Debug.Log(e.data.ToString());
        socket.Emit("joinro");
    }

    public void SetNickname(string nickname)
    {
        NetworkMain.inst.nickname = nickname;
    }

    void OnDisable()
    {
        socket.Off("joinr", JoinRoom);
        socket.Off("joiGame", JoinGame);
    }
}

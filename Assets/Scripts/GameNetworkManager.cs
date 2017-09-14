using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager inst;

    SocketIOComponent socket;

    public GameObject playerPrefab;
    public Transform spawnPoint;

    public List<Player> players = new List<Player>();

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        socket = NetworkMain.inst.socket;
        Init();
    }

    void Init()
    {
        socket.On("gamecoor", RecivePlayerPosition);
    }

    void RecivePlayerPosition(SocketIOEvent e)
    {
        PlayerPositionInfo playerInfo = PlayerPositionInfo.FromJson(e.data.ToString());
        bool isExist = false;

        for (int i = 0; i < players.Count; i++)
        {
            if (playerInfo.nick == players[i].info.nick)
            {
                isExist = true;
                players[i].RecivePlayerInfo(playerInfo);
            }
        }
        if (isExist)
            return;

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Player player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Player>();
        players.Add(player);
    }

    public void SendPlayerPos(PlayerPositionInfo pos)
    {
        string jsonStr = JsonUtility.ToJson(pos);
        socket.Emit("game", new JSONObject(jsonStr));
    }

    void OnDisable()
    {
        socket.Off("gamecoor", RecivePlayerPosition);
    }
}

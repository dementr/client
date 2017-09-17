using UnityEngine;
using SocketIO;

public class ProfileMenu : MonoBehaviour
{
    //SocketIOComponent socket;

    void Awake()
    {
        //socket = NetworkMain.inst.socket;
    }

    public void SetNickname(string nickname)
    {
        NetworkMain.inst.nickname = nickname;
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }
}

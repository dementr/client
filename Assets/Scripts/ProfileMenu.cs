using UnityEngine;
using TMPro;
using SocketIO;

public class ProfileMenu : MonoBehaviour
{
    //SocketIOComponent socket;
    public TMP_InputField nickInput;

    public void SetNickname(string nickname)
    {
        NetworkMain.inst.nickname = nickname;
    }

    void OnEnable()
    {
        nickInput.text = NetworkMain.inst.nickname;
        //socket = NetworkMain.inst.socket;
    }

    void OnDisable()
    {

    }
}

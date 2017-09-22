using UnityEngine;
using TMPro;
using SocketIO;

public class ProfileMenu : MonoBehaviour
{
    //SocketIOComponent socket;
    public TMP_InputField nickInput;
    public TMP_Text onlineCountText;

    public void SetNickname(string nickname)
    {
        NetworkMain.inst.nickname = nickname;
    }

    void OnOnlineChanges(string count)
    {
        onlineCountText.text = "Online: " + count;
    }

    void OnEnable()
    {
        //socket = NetworkMain.inst.socket;
        nickInput.text = NetworkMain.inst.nickname;

        OnOnlineChanges(NetworkMain.inst.onlineCount);

        NetworkMain.inst.OnOnlineChanged += OnOnlineChanges;

        //socket.On("countCon", OnOnlineChanges);
    }

    void OnDisable()
    {
        NetworkMain.inst.OnOnlineChanged -= OnOnlineChanges;
        //socket.Off("countCon", OnOnlineChanges);
    }
}

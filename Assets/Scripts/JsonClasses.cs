using UnityEngine;
using System.Linq;

public class LoginInfo
{
    public string login;
    public string password;

    public LoginInfo(string _login, string _pass)
    {
        login = _login;
        password = _pass;
    }
}

public class RegistrationInfoBase
{
    public string email;
    public string password;

    public RegistrationInfoBase(RegistrationInfo regInfo)
    {
        email = regInfo.email;
        password = regInfo.password;
    }
}

[System.Serializable]
public class RegistrationInfo : RegistrationInfoBase
{
    public string confirmPassword;

    public RegistrationInfo(RegistrationInfo regInfo)
: base(regInfo)
    {

    }

    public bool CheckPasswordCorrectness()
    {
        bool digitIsPresent = password.Where(c => char.IsDigit(c)).Count() > 0;

        if (password.Length >= 6 && digitIsPresent)
        {
            return true;
        }

        return false;
    }

    public bool CheckPassordIdentity()
    {
        if (password.Length != confirmPassword.Length)
            return false;

        if (password == confirmPassword)
            return true;

        return false;
    }
}

public class AuthInfo
{
    public string key;
    public string nick;

    public static AuthInfo FromJson(string json)
    {
        return JsonUtility.FromJson<AuthInfo>(json);
    }
}

public class SetNickInfo
{
    public string result;

    public static SetNickInfo FromJson(string json)
    {
        return JsonUtility.FromJson<SetNickInfo>(json);
    }
}

public class SendedChatMsg
{
    public string msg;

    public SendedChatMsg(string _msg)
    {
        msg = _msg;
    }
}

public class RecivedChatMsg
{
    public string nick;
    public string msg;

    public static RecivedChatMsg FromJson(string jsString)
    {
        return JsonUtility.FromJson<RecivedChatMsg>(jsString);
    }
}

public class PlayerPositionInfo
{
    public string nick;
    public Vector3 pos;

    public PlayerPositionInfo(string _nick, Vector3 _pos)
    {
        nick = _nick;
        pos = _pos;
    }

    public static PlayerPositionInfo FromJson(string jSon)
    {
        return JsonUtility.FromJson<PlayerPositionInfo>(jSon);
    }
}

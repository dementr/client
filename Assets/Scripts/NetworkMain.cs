#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkMain : MonoBehaviour
{
    public static NetworkMain inst;

    public SocketIOComponent socket;

    public string login;
    public string password;
    public string nickname;

    void Awake()
    {
        inst = this;
    }

    public void Init()
    {
        socket.On("open", TestOpen);
        socket.On("error", TestError);
        socket.On("close", TestClose);
    }

    public void SetLogin(string _login)
    {
        NetworkMain.inst.login = _login;
    }

    public void SetPassword(string _password)
    {
        NetworkMain.inst.password = _password;
    }

    #region Callbacks

    public void TestOpen(SocketIOEvent e)
    {
        Debug.Log("OPEN");
        //Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    public void TestError(SocketIOEvent e)
    {
        Debug.Log("ERROR");
        //Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("CLOSE");
        //Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
    }

    #endregion
}

#region Json classes

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

public class Test
{
    public string hello;
    public int is1;

    public static Test FromJson(string jsonStr)
    {
        return JsonUtility.FromJson<Test>(jsonStr);
    }
}
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerPositionInfo info;

    public float speed;

    void Start()
    {
        StartCoroutine(SendPosition());
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        info.pos += new Vector3(x, 0, z) * speed * Time.deltaTime;
    }

    public void RecivePlayerInfo(PlayerPositionInfo _info)
    {
        info = _info;
        transform.position = info.pos;
    }

    IEnumerator SendPosition()
    {
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                GameNetworkManager.inst.SendPlayerPos(info);
            }
            yield return new WaitForSeconds(1);
        }
    }
}

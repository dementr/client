using System.Collections;
using UnityEngine;

public class ServerSimulation : MonoBehaviour
{
    public PlayerSimulation player;

    public Vector3 curPlayerPos;

    WaitForSeconds delay = new WaitForSeconds(0.2f);

    public void SetStartPos(PlayerSimulation _player)
    {
        player = _player;
        curPlayerPos = _player.transform.position;
    }

    public void InputFromPlayer(Vector3 dir, Vector3 pos)
    {
        StartCoroutine(Delayer(dir, pos));
    }

    IEnumerator Delayer(Vector3 dir, Vector3 pos)
    {
        yield return delay;

        float dist = (pos - curPlayerPos).sqrMagnitude;

        curPlayerPos += dir;
        Debug.Log(dist);

        if (dist > 0.5f)
        {
            player.RecivePos(curPlayerPos);
        }
    }
}

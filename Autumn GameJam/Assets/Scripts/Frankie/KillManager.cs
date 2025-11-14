using UnityEngine;

public class EnemyKillManager : MonoBehaviour
{
    public int currentKills = 0;

    public void AddKill()
    {
        currentKills++;
    }
}

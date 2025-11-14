using UnityEngine;

public class EnemyCountDoor : MonoBehaviour
{
    public int killsRequired = 5;
    public EnemyKillManager killManager;
    public float playerDistanceToUse = 3f;
    public Transform player;  // drag your player here!

    void Update()
    {
        // Only allow opening if player is close enough
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= playerDistanceToUse && Input.GetKeyDown(KeyCode.E))
        {
            if (killManager.currentKills >= killsRequired)
            {
                GetComponent<Animator>().SetTrigger("OpenDoor");
            }
            else
            {
                Debug.Log("Door locked. Kill more enemies.");
            }
        }
    }
}

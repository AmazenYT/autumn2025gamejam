using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCountDoor : MonoBehaviour
{
    [Header("Enemy To Kill Before Door Unlocks")]
    public GameObject requiredEnemy;   // drag the specific enemy here

    [Header("Player Settings")]
    public Transform player;           
    public float playerDistanceToUse = 3f;

    [Header("Victory Scene")]
    public string victorySceneName = "VictoryScreen";

    void Update()
    {
        // If the enemy still exists, door stays locked
        if (requiredEnemy != null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= playerDistanceToUse && Input.GetKeyDown(KeyCode.E))
        {
            LoadVictoryScene();
        }
    }

    void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }
}

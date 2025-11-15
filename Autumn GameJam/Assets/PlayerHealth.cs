using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public GameOverScreen GameOverScreen;
    public EnemyKillManager enemyKillManager;
    public void TakeDamagePlayer(float damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0) Invoke(nameof(KillPlayer), 0.5f);
    }

    public void KillPlayer()
    {
        Debug.Log("Player Dead");
        GameOverScreen.Setup(enemyKillManager.currentKills);
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float damage = 50;

    void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            playerHealth.TakeDamagePlayer(damage);
        }
    }
}

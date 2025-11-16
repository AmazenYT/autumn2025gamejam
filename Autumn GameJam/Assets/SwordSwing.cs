using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class SwordSwing : MonoBehaviour
{
    public int damage = 25;

    public Transform attackPoint;
    public float attackRange = 10;

    public LayerMask enemyLayers;
    void Update()
    {
            
    if (Input.GetMouseButtonDown(0))
    {
        Debug.Log("Attack");
        Attack();
    }
        
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("Enemy hit with sword");
                enemy.GetComponent<EnemyScript>().TakeDamage(damage);
            }

        Debug.Log("huh???");
    }
}

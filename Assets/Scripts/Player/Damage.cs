using UnityEngine;

public class Damage : MonoBehaviour
{
    private int damageAmount=1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<EnemiAi>())
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.KenaDamage(20);
        }

    }
}

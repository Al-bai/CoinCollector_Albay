using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Referensi UI")]
    public HealthBar healthBar; 

    void Start()
    {
        currentHealth = maxHealth;
    
        healthBar.SetMaxHealth(maxHealth);
    }


    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

       
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

     
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player Mati!");
        string sceneSekarang = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneSekarang);
    }
}
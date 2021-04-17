using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 0;
    [SerializeField] private int currentHealth = 0;
    [SerializeField] private TMP_Text healthScore = null;

    // Start is called before the first frame update
    private void Start()
    {
        healthScore.text = currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        int newHealth = currentHealth - damage;
        if(newHealth <= 0)
        {
            Debug.Log("Dead");
        }
        else
        {
            currentHealth = newHealth;
            healthScore.text = currentHealth.ToString();
        }
    }
}

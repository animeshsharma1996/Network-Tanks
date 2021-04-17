using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 0;

    [SyncVar(hook = "OnHealthChanged")]
    [SerializeField] private int currentHealth = 0;
    [SerializeField] private TMP_Text healthScore = null;

    // Start is called before the first frame update
    private void Start()
    {
        healthScore.text = currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        if(!isServer)  {  return;  }

        int newHealth = currentHealth - damage;
        if(newHealth <= 0)
        {
            Debug.Log("Dead");
        }
        else
        {
            currentHealth = newHealth;
        }
    }

    private void OnHealthChanged(int updatedHealth)
    {
        healthScore.text = updatedHealth.ToString();
    }
}

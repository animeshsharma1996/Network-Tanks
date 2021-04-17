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

    private NetworkStartPosition[] spawnPoints = null;

    private void Start()
    {
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        healthScore.text = currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        if(!isServer)  {  return;  }

        int newHealth = currentHealth - damage;
        if(newHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcDeath();
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

    [ClientRpc]
    private void RpcDeath()
    {
        if(isLocalPlayer)
        {
            int chosenPoint = Random.Range(0,spawnPoints.Length);
            transform.position = spawnPoints[chosenPoint].transform.position;
        }
    }
}

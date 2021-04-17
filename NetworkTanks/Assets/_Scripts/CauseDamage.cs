using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    [SerializeField] private int maxDamage = 10;

    public int GetDamage()
    {
        return maxDamage;
    }
}

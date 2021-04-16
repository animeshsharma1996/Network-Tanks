using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] private float defaultDeathTime = 2.0f;

    private void Start()
    {
        StartCoroutine(DisableShot(defaultDeathTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    private IEnumerator DisableShot(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

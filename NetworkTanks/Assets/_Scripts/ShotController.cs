using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] private float defaultDeathTime = 2.0f;

    private Rigidbody shotRigidbody;

    private void Start()
    {
        shotRigidbody = GetComponent<Rigidbody>();
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
        shotRigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

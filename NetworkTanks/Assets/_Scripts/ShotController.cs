using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class ShotController : NetworkBehaviour
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

    private IEnumerator DisableShot(float time)
    {
        yield return new WaitForSeconds(time);
        shotRigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

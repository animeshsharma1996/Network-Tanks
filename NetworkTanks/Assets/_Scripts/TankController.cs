using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class TankController : NetworkBehaviour 
{
	[SerializeField] private float MoveSpeed = 150.0f;
	[SerializeField] private float RotateSpeed = 3.0f;
	[SerializeField] private Color localPlayerColor = Color.white;

	[SerializeField] private GameObject shotPrefab;
	[SerializeField] private Transform shotSpawnTransform;
	[SerializeField] private float shotSpeed = 0.0f;
	[SerializeField] private float reloadRate = 0.5f;
	[SerializeField] private int poolSize = 0;

	private List<GameObject> shotPool = new List<GameObject>();
	private List<Rigidbody> rigidbodyShotPool = new List<Rigidbody>();
	private int shotCounter = 0;
	private float nextShotTime;

	private void Start()
    {
        shotCounter = 0;
        CmdCreateShotPool();
    }

    [Command]
    private void CmdCreateShotPool()
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject shotBullet = Instantiate(shotPrefab, shotSpawnTransform.position, Quaternion.identity);
            shotPool.Add(shotBullet);
            rigidbodyShotPool.Add(shotBullet.GetComponent<Rigidbody>());
            NetworkServer.Spawn(shotBullet);
            shotBullet.SetActive(false);
        }
    }

    public override void OnStartLocalPlayer()
    {
		MeshRenderer[] tankParts = GetComponentsInChildren<MeshRenderer>();

		foreach(MeshRenderer tankPart in tankParts)
        {
			tankPart.material.color = localPlayerColor;
        }
    }

	private void Update () 
	{
		if (!isLocalPlayer) { return; }

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * RotateSpeed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);

		if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextShotTime)
        {
            Fire();
        }
    }

    
    private void Fire()
    {
        nextShotTime = Time.time + reloadRate;
        Debug.Log(shotCounter);
        if (shotCounter < poolSize)
        {
            CmdShootProjectile();
        }
        else
        {
            shotCounter = 0;
            ResetShotProjectPool();
            CmdShootProjectile();
        }
    }

    [Command]
    private void CmdShootProjectile()
    {
        shotPool[shotCounter].SetActive(true);
        shotPool[shotCounter].transform.position = shotSpawnTransform.position;
        rigidbodyShotPool[shotCounter].velocity = transform.forward * shotSpeed;
        ++shotCounter;
    }

    private void ResetShotProjectPool()
    {
        foreach (GameObject shotProjectile in shotPool)
        {
            shotProjectile.transform.position = shotSpawnTransform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        try
        {
            CauseDamage causeDamageScript = other.GetComponent<CauseDamage>();
            int totalDamage = causeDamageScript.GetDamage();
            Health healthScript = GetComponent<Health>();
            healthScript.TakeDamage(totalDamage);
        }
        catch
        {
            Debug.Log("Somthing hit us but didn't do any damage");
        }
    }
}

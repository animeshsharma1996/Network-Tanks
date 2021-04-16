using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class TankController : NetworkBehaviour 
{
	[SerializeField] private float MoveSpeed = 150.0f;
	[SerializeField] private float RotateSpeed = 3.0f;
	[SerializeField] private Color localPlayerColor = Color.white;

	private void Start () 
	{
		
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

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * RotateSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
}

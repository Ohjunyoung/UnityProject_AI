using UnityEngine;
using System.Collections;

public class PlayerTankController : MonoBehaviour {

	public GameObject bullet;

	private Transform turret;
	private Transform bulletSpawnPoint;
	private float curSpeed, targetSpeed, rotSpeed;
	private float turretRotSpped = 10.1f;
	private float maxForwardSpeed = 300.0f;
	private float maxBackwardSpeed = -300.0f;

	protected float shootRate = 0.1f;
	protected float elapseTime;



	// Use this for initialization
	void Start () {
		rotSpeed = 150.0f;
		turret = gameObject.transform.FindChild("Turret").transform;
		bulletSpawnPoint = turret.transform.FindChild("SpawnPoint").transform;

	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateWeapon ();
		UpdateControl ();
	}

	void UpdateWeapon()
	{
		if (Input.GetMouseButton (0))
		{
			elapseTime += Time.deltaTime;

			if( elapseTime >= shootRate)
			{
				elapseTime = 0;
				Instantiate(bullet,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
			}
		}
	}
	void UpdateControl()
	{
		Plane playerPlane = new Plane (Vector3.up, gameObject.transform.position);
		Ray rayCast = Camera.main.ScreenPointToRay (Input.mousePosition);

		float hitDist = 0;
		if(playerPlane.Raycast(rayCast,out hitDist))
		{
			Vector3 rayHitPoint = rayCast.GetPoint(hitDist);
			Quaternion targetRotation =  Quaternion.LookRotation(rayHitPoint - transform.position);

			turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation,targetRotation,
			                                             Time.deltaTime * turretRotSpped);

		}


		if(Input.GetKey(KeyCode.W))
		{
			targetSpeed = maxForwardSpeed;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			targetSpeed = maxBackwardSpeed;
		}
		else
		{
			targetSpeed = 0;
		}

		if (Input.GetKey (KeyCode.A))
		{
			transform.Rotate(0,-rotSpeed * Time.deltaTime, 0.0f);
		} 
		else if( Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,rotSpeed * Time.deltaTime,0.0f);
		}

		curSpeed = Mathf.Lerp (curSpeed, targetSpeed, 3.0f * Time.deltaTime);

		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed);

	}
}

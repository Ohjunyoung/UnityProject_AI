using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject Explosion;

	public float Speed = 600.0f;
	public  float lifeTime = 10.0f;
	public int damage = 50;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Speed * Time.deltaTime;
	}

	void OnCollisionEnter(Collision col)
	{
		ContactPoint contact = col.contacts [0];
		Instantiate (Explosion, contact.point, Quaternion.identity);
		Destroy (gameObject);
	}
}

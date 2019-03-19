using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;
	public float speed = 6f;
	public float rotation = .7f;

	public float jumpHeight = 14f;
	private bool isFalling = false;
	private Vector3 AddJump;
	public GameObject[] Weapons;

	public PlayerBow bowAmmo;
	public PlayerPotion potionAmmo;

	public float playerHealth = 100f;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}
		
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal") * rotation;

		float moveVertical = Input.GetAxis ("Vertical") * speed;

		Vector3 movement = new Vector3 (0.0f, 0.0f, moveVertical);

		rb.AddRelativeForce (movement);

		rb.AddTorque (0, moveHorizontal, 0);

		if (Input.GetKeyDown (KeyCode.Space) && !isFalling) 
		{
			AddJump.y = jumpHeight;
			GetComponent<Rigidbody> ().velocity += AddJump;

		}
		isFalling = true;
	}

	public void OnCollisionStay()
	{
		isFalling = false;
	}

	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			Debug.Log ("Key Pressed");
			foreach (GameObject wp in Weapons) 
			{
				if (wp.name == "Sword")
					wp.gameObject.SetActive (true);
				else
					wp.gameObject.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) 
		{
			Debug.Log ("Key Pressed");
			foreach (GameObject wp in Weapons) 
			{
				if (wp.name == "Bow")
					wp.gameObject.SetActive (true);
				else
					wp.gameObject.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) 
		{
			Debug.Log ("Key Pressed");
			foreach (GameObject wp in Weapons) 
			{
				if (wp.name == "Potion")
					wp.gameObject.SetActive (true);
				else
					wp.gameObject.SetActive (false);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Ammo")) 
		{
			bowAmmo.currentAmmo += 1;
			Debug.Log ("Adding Arrow");
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("Potion")) 
		{
			potionAmmo.currentPotions += 1;
			Debug.Log ("Adding Potion");
			Destroy (other.gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;
	private Scene scene;

	public float playerHealth;

	public float speed = 6f;
	public float rotation = .7f;
	public float jumpHeight = 14f;
	private bool isFalling = false;
	private Vector3 AddJump;

	public GameObject[] Weapons;

	public float throwForce = 44f;
	public float upForce = 10f;
	public GameObject potionPrefab;

	public int maxPotions = 3;
	public int currentPotions = 0;
	private bool hasPotions = true;
	private bool isUsingPotion = false;

	public PlayerBow bowAmmo;
	public PlayerPotion potionAmmo;

	public Text healthText;
	public Text arrowText;
	public Text potionText;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		scene = SceneManager.GetActiveScene();
		playerHealth = 100;

		currentPotions = maxPotions;
		/*
		SetHealthText ();
		SetArrowText ();
		SetPotionText ();
		*/
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
			//playerHealth -= 20;
			/*
			SetHealthText ();
			*/
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
				if (wp.name == "Sword") {
					wp.gameObject.SetActive (true);
					isUsingPotion = false;
				}
				else
					wp.gameObject.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) 
		{
			speed = speed - 2;
			Debug.Log ("Key Pressed");
			foreach (GameObject wp in Weapons) 
			{
				if (wp.name == "Bow") {
					wp.gameObject.SetActive (true);
					isUsingPotion = false;
				}
				else
					wp.gameObject.SetActive (false);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) 
		{
			Debug.Log ("Key Pressed");
			foreach (GameObject wp in Weapons) 
			{
				if (wp.name == "Potion") {
					
					wp.gameObject.SetActive (true);
					isUsingPotion = true;
				}
				else
					wp.gameObject.SetActive (false);
			}
		}
		if (playerHealth > 0) 
		{
			playerHealth = 100;
		}

		if (playerHealth <= 0) 
		{
			SceneManager.LoadScene ("Scene0");
		}

		if (Input.GetKeyDown (KeyCode.F) && hasPotions && isUsingPotion) 
		{
			ThrowPotion ();
		}

		if (currentPotions >= 1) 
		{
			hasPotions = true;
		}

		if (currentPotions > maxPotions) 
		{
			currentPotions = maxPotions;
		}

		/*
		SetHealthText ();
		SetArrowText ();
		SetPotionText ();
		*/
	}

	void LateUpdate()
	{
		if (currentPotions <= 0) 
		{
			hasPotions = false;
		}
	}

	void ThrowPotion()
	{
		Transform player = GameObject.Find ("Player").GetComponent <Transform> ();
		GameObject potion = Instantiate (potionPrefab, transform.position, Quaternion.identity);

		Rigidbody rb = potion.GetComponent<Rigidbody> ();

		rb.AddRelativeForce (transform.up * (throwForce / 2));
		rb.AddForce (player.forward * throwForce, ForceMode.Force);

		currentPotions--;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Ammo")) 
		{
			bowAmmo.currentAmmo += 1;
			Debug.Log ("Adding Arrow");
			/*
			SetArrowText ();
			*/
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("Potion")) 
		{
			currentPotions += 1;
			Debug.Log ("Adding Potion");
			/*
			SetPotionText ();
			*/
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("Healing")) 
		{
			if (playerHealth < 100) 
			{
				playerHealth += 40;
				/*
				SetHealthText ();
				*/
				Destroy (other.gameObject);
				/*
					SetHealthText ();
					*/					
			}
			Destroy (other.gameObject);
		}
	}
	/*
	void SetHealthText()
	{
		healthText.text = "Player's Health is: " + playerHealth.ToString ("f0");
	}
	void SetArrowText()
	{
		arrowText.text = "Current # of Arrows: " + bowAmmo.currentAmmo.ToString ("f0");
	}
	void SetPotionText()
	{
		potionText.text = "Current Potions: " + potionAmmo.currentPotions.ToString ("f0");
	}*/
}

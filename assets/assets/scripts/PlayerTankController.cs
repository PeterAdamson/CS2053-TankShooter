using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour {

	private Vector3 velocity;
	private bool canFire;
	public GameObject bullet;

	private SpriteRenderer rend;
	//private Animator anim;
	public float speed = 2.0f;

	public GameController gameController;

	// Use this for initialization
	void Start ()
	{
		velocity = new Vector3(0f, 0f, 0f);
		rend = GetComponent<SpriteRenderer> ();
		canFire = true;
	}

	// Update is called once per frame
	void Update ()
	{
		// calculate location of screen borders
		 // this will make more sense after we discuss vectors and 3D
		 var dist = (transform.position - Camera.main.transform.position).z;
		 var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		 var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

		 //get the width of the object
		 float width = rend.bounds.size.x;
		 float height = rend.bounds.size.y;

		 //set the direction based on  input
		 if (Input.GetKey("left") || Input.GetKey("a") || Input.GetKey("up") || Input.GetKey("w"))
		 {
				 velocity = new Vector3(-1f, 0f, 0f);
		 }
		 if (Input.GetKey("right") || Input.GetKey("d") || Input.GetKey("down") || Input.GetKey("s"))
		 {
				 velocity = new Vector3(1f, 0f, 0f);
		 }

		 //shooting the bullet from the player
		 if (Input.GetMouseButtonDown(0) && canFire)
		 {
				//the offset
				Vector3 offset = new Vector3(2f, 4f, 0f);
				GameObject b = Instantiate(bullet, new Vector3 (0f, 0f, 0f), Quaternion.identity);
				b.GetComponent<BulletControllerPlayer>().InitPosition(transform.position + offset, new Vector3(0f,2f,0f));
				canFire = false;
				StartCoroutine(PlayerCanFireAgain());
		}

		 //make sure the obect is inside the borders... if edge is hit stop
		 if((transform.position.x - 0.5 <= leftBorder + width/2.0) && velocity.x < 0f){
				 velocity = new Vector3(0f, 0f, 0f);
		 }
		 if((transform.position.x + 1.5 >= rightBorder - width/2.0) && velocity.x > 0f){
				 velocity = new Vector3(0f, 0f, 0f);
		 }

		 transform.position = transform.position + velocity * Time.deltaTime * speed;
	}

	//will wait 3 seconds and then will reset the flag
	IEnumerator PlayerCanFireAgain()
	{
			yield return new WaitForSecondsRealtime(3);
			canFire = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		gameController.playerHit();
	}
}

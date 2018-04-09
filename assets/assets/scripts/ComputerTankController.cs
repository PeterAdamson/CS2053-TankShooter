using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTankController : MonoBehaviour {

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
		velocity = new Vector3(1f, 0f, 0f);
		rend = GetComponent<SpriteRenderer> ();
		canFire = true;
	}

	//update is called once per frame
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

		 //1% of the time, switch the direction:
		 int change = Random.Range(0,100);
		 if (change == 0)
		 {
				 velocity = new Vector3(-1f, 0f, 0);
		 }

		 //shooting the bullet from the computer
		 if (canFire)
		 {
				//the offset
				Vector3 offset = new Vector3(4f, 1.5f, 0f);
				GameObject b = Instantiate(bullet, new Vector3 (0f, 0f, 0f), Quaternion.identity);
				b.GetComponent<BulletControllerComputer>().InitPosition(transform.position + offset, new Vector3(0f,2f,0f));
				canFire = false;
				StartCoroutine(ComputerCanFireAgain());
		}

			//make sure the obect is inside the borders... if edge is hit reverse direction
			if((transform.position.x - 0.5 <= leftBorder + width/2.0) && velocity.x < 0f){
					velocity = new Vector3(1f, 0f, 0f);
			}
			if((transform.position.x + 1.5 >= rightBorder - width/2.0) && velocity.x > 0f){
					velocity = new Vector3(-1f, 0f, 0f);
			}
			transform.position = transform.position + velocity * Time.deltaTime * speed;
	}

	//will wait 3 seconds and then will reset the flag
	IEnumerator ComputerCanFireAgain()
	{
			yield return new WaitForSecondsRealtime(3);
			canFire = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		gameController.computerHit();
	}
}

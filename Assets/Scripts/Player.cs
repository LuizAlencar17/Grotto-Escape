using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxSpeed;
	public float jumpForce;
	
	private bool grounded;
	private bool jumping;

	float move;

	private Rigidbody2D rb2d;
	private Animator anim;
	private SpriteRenderer sprite;
	
	public Transform groundCheck;
	
	//Variaveis p Tiro
	public Transform bulletSpawn;
	public GameObject bulletObject;
	public float fireRate;
	public float nextFire;

	// Use this for initializatioan
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if(Input.GetButtonDown("Jump") && grounded){
			jumping =true;
		}
		if(Input.GetButton("Fire1") && Time.time > nextFire){
			Fire();
		}
	}

	void Flip (){
		sprite.flipX = !sprite.flipX;
		if (!sprite.flipX) {
			bulletSpawn.position = new Vector3 (this.transform.position.x + 0.2f, bulletSpawn.position.y, bulletSpawn.position.z);
		} else {
			bulletSpawn.position = new Vector3 (this.transform.position.x - 0.2f, bulletSpawn.position.y, bulletSpawn.position.z);
		}
	}

	void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

	}
	void FixedUpdate(){

		move = Input.GetAxis("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs(move));

		rb2d.velocity = new Vector2(move*maxSpeed, rb2d.velocity.y);

		if((move > 0f && sprite.flipX) || (move < 0f && !sprite.flipX)){
			Flip();
		}
		anim.SetBool("JumpFall", rb2d.velocity.y != 0f);
		if(jumping){
			rb2d.AddForce(new Vector2(0f,jumpForce));
			jumping = false;
			
		}
	}

	void Fire(){
		anim.SetTrigger ("Fire");

		nextFire = Time.time+fireRate;

		GameObject cloneBullet = Instantiate (bulletObject, bulletSpawn.position, bulletSpawn.rotation);
		
		if (sprite.flipX) {
			cloneBullet.transform.eulerAngles = new Vector3 (0,0,180);
		}

	}

}
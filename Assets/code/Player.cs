using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public int ply_HP = 100;
	private Animator animator;
	public float movePower = 1f;
	public float m_jumpforce = 0f;
	public int m_maxjumpcount = 0;
	float m_distance = 0f;
	public LayerMask m_layerMask = 0;
	//int m_jumpCount = 0;
	public Transform wallCHk;
	public Transform wallCHk2;
	public Transform bottomCHK;

	public float wallchkDistance;
	public float movespeed;
	public LayerMask W_layer;
	int isRight = 1;
	public float slidingSpeed;
	bool nodamaged=true;
	public bool iswall;
	public bool iswall2;
	public bool isbottom;
	public float wallJumppower;
	public float maxspeed;
	public KeyCode sit;
	public bool direction;

	SpriteRenderer SpriteRenderer2d;
	Rigidbody2D rigid;

	Vector3 movement; 

	private void Awake() //sssss
    {
        animator = GetComponent<Animator>();
    }

	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D>();
		m_distance = GetComponent <CapsuleCollider2D>().bounds.extents.y + 0.05f;
		SpriteRenderer2d = GetComponent<SpriteRenderer>();
	}

	//Graphic & Input Updates	
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		isbottom = Physics2D.Raycast(bottomCHK.position, Vector2.down, wallchkDistance, W_layer);
		if (Input.GetButtonDown("Horizontal")){
			animator.SetBool("run?", true);
		}
		if (Input.GetButtonUp("Horizontal")){
			animator.SetBool("run?", false);
		}
		Jump();
		if (Input.GetButtonDown("Jump")){
			//animator.SetBool("run?", false);
			if(isbottom)
				animator.SetBool("jump?", true);
		}
		
		if (horizontal == -1){
			animator.SetBool("run?", true);
			direction=true;
		}
		else if (horizontal == 1){
			animator.SetBool("run?", true);
			direction=false;
		}
		
		
		SpriteRenderer2d.flipX = direction;
		
		iswall = Physics2D.Raycast(wallCHk.position, Vector2.right, wallchkDistance, W_layer);
		
		iswall2 = Physics2D.Raycast(wallCHk2.position, Vector2.right, wallchkDistance, W_layer);
		if (Input.GetButton("sit")){
			animator.SetBool("sit?", true);
			movespeed=1;
			maxspeed=2;
			slidingSpeed=1f;
		}
		else{
			animator.SetBool("sit?", false);
			slidingSpeed=0.8f;
			movespeed=2;
			maxspeed=5;
		}
	}
	private void FixedUpdate()
	{
		Move();
		if (iswall || iswall2)
		{
			if (iswall) rigid.AddForce(Vector3.right*5);
			else rigid.AddForce(Vector3.left*5);


			rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
			if (Input.GetAxis("Jump") != 0)
			{
				rigid.velocity = new Vector2(rigid.velocity.x, 0.9f * wallJumppower);
			}
		}
	}


    void OnTriggerStay2D(Collider2D collision)	{
		if (collision.gameObject.tag == "Enemy")
		{
			if (nodamaged){
				OnDamaged(collision.transform.position);
			}
			/*if (collision.gameObject.transform.position.x < gameObject.transform.position.x){
				rigid.AddForce(Vector3.right*300);
				rigid.AddForce(Vector3.up*15);
			}
			else {
				rigid.AddForce(Vector3.left*300);
				rigid.AddForce(Vector3.up*15);
			}*/
		}
	}
	void OnDamaged(Vector2 targetPos)
	{
		ply_HP -= 10;
		//gameObject.layer = 8;
		SpriteRenderer2d.color = new Color(1, 1, 1, 0.4f);

		//�ι�
		int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
		rigid.AddForce(Vector3.right*300*dirc );
		rigid.AddForce(Vector3.up*15);
		nodamaged = false;
		Invoke("offdamage", 2);
	}
	void offdamage()
	{
		//gameObject.layer = 6;
		nodamaged = true;
		SpriteRenderer2d.color = new Color(1, 1, 1, 1);
	}

	void Move()
	{
		/*
		Vector3 moveVelocity = Vector3.zero;

		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			isRight = -1;
			moveVelocity = Vector3.left;
		}

		else if (Input.GetAxisRaw("Horizontal") > 0)
		{
			isRight = 1;
			moveVelocity = Vector3.right;
		}

		transform.position += moveVelocity * movePower * Time.deltaTime;
		*/
		float horizontal = Input.GetAxisRaw("Horizontal");
		rigid.AddForce(new Vector2(0.5f,0) * movespeed * horizontal, ForceMode2D.Impulse);
		
		//rigid.AddForce(Vector3.up*800f);

		if (rigid.velocity.x > maxspeed) rigid.velocity = new Vector2(maxspeed,rigid.velocity.y);
		else if (rigid.velocity.x < maxspeed*(-1)) rigid.velocity = new Vector2(maxspeed*(-1),rigid.velocity.y);

	}

	void Jump()
	{
		animator.SetBool("jump?", false);
		if(Input.GetAxis("Jump") != 0)
		{
			if(isbottom)
			{
				animator.SetBool("jump?", true);
                rigid.velocity = new Vector2(0,1) * m_jumpforce;
				//Debug.Log("aa");
			}
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(wallCHk.position, Vector2.right * isRight * wallchkDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(wallCHk2.position, Vector2.right * isRight * wallchkDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(bottomCHK.position, Vector2.down * wallchkDistance);
	}
}

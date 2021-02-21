using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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
	bool downtownbaby=true;
	public bool iswall;
	public bool iswall2;
	public bool isbottom;
	
	public float wallJumppower;
	public float maxspeed;
	public float downstart;
	public KeyCode sit;
	public bool direction;
	public bool die; //die
	public float clmb_speed = 0.0f;

	SpriteRenderer SpriteRenderer2d;
	Rigidbody2D rigid;

	Vector3 movement;

	public GameObject Effect_smoke;
	public Text ScriptTxt;

	private void Awake() //sssss
    {
        animator = GetComponent<Animator>();
		
    }

	void Start()
	{
		Application.targetFrameRate=60;
		//effect(); #on//of
		ScriptTxt.text = "";
		rigid = gameObject.GetComponent<Rigidbody2D>();
		m_distance = GetComponent <CapsuleCollider2D>().bounds.extents.y + 0.05f;
		SpriteRenderer2d = GetComponent<SpriteRenderer>();
	}

	//Graphic & Input Updates	
	void Update()
	{
		if (die == true) return;
		float horizontal = Input.GetAxisRaw("Horizontal");
		isbottom = Physics2D.Raycast(bottomCHK.position, Vector2.down, wallchkDistance, W_layer);
		Animation();
		JumpAnimation();
		Isground();
		SpriteRenderer2d.flipX = direction;
		
		iswall = Physics2D.Raycast(wallCHk.position, Vector2.right, wallchkDistance, W_layer);
		
		iswall2 = Physics2D.Raycast(wallCHk2.position, Vector2.right, wallchkDistance, W_layer);
		if (Input.GetButton("sit")){
			if (isbottom||iswall||iswall2){
			animator.SetBool("jump?", false);
			animator.SetBool("sit?", true);
			movespeed=1;
			maxspeed=2;
			slidingSpeed=1f;
			}
		}
		if (Input.GetButtonUp("sit")){
			animator.SetBool("sit?", false);
			slidingSpeed=0.8f;
			movespeed=2;
			maxspeed=5;
		}
	}

	void Animation()
    {
		if (Input.GetButtonDown("Horizontal"))
		{
			animator.SetBool("run?", true);

		}
		if (Input.GetButtonUp("Horizontal"))
		{
			animator.SetBool("run?", false);
		}
        
		

	}
	private void FixedUpdate()
	{
		if (die == true) return;
		Move();
		Die();
		if ((iswall && direction == false)|| (iswall2 && direction == true))
		{
			if (iswall) rigid.AddForce(Vector3.right*5);
			else rigid.AddForce(Vector3.left*5);
			animator.SetBool("clmb?", true);

			rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
			if (Input.GetAxis("Jump") != 0)
			{
				animator.SetBool("clmbing", true);
				clmb_speed = 1f;
				rigid.velocity = new Vector2(rigid.velocity.x, 0.9f * wallJumppower);
			}
			else animator.SetBool("clmbing", false);
			
			
		}
		else {animator.SetBool("clmb?", false);animator.SetBool("clmbing", false);}
		
		

	}


    void OnTriggerEnter2D(Collider2D collision)	{
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
		ScriptTxt.text = "아야 존나아프네 씨ㅣㅂ라";
		ply_HP -= 10;
		//gameObject.layer = 8;
		SpriteRenderer2d.color = new Color(1, 1, 1, 0.4f);

		//�ι�
		int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
		rigid.AddForce(Vector3.right*300*dirc );
		rigid.AddForce(Vector3.up*15);
		nodamaged = false;
		animator.SetTrigger("hurt");
		Debug.Log("hurt");
		Invoke("offdamage", 1.2f);
	}
	void offdamage()
	{
		ScriptTxt.text = "";
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
		//Debug.Log(rigid.velocity.y);
		if (rigid.velocity.y<downstart) {
			if (downtownbaby){
				animator.SetTrigger("down");
				downtownbaby=false;
				}
		}
		else downtownbaby=true;
		

		if (horizontal == -1)
		{
			animator.SetBool("run?", true);
			direction = true;
		}
		else if (horizontal == 1)
		{
			animator.SetBool("run?", true);
			direction = false;
		}
		if (Input.GetButtonDown("Jump"))
		{
			if (isbottom)
			{
				animator.SetBool("jump?", true);
			}
		}

	}

	void Die()
	{
		if (ply_HP <= 0)
		{
			die = true;
			animator.SetTrigger("die");
		}

	}

	void JumpAnimation()
	{
		if (Input.GetButton("sit")!=true){
			if (Input.GetAxisRaw("Jump") != 0)
			{
				
				if(isbottom)
				{
					//Debug.Log("점프");
					rigid.velocity = new Vector2(0,1) * m_jumpforce;
					animator.SetTrigger("jump?");
					//Debug.Log("aa");
				}
				
			}
		}
		

		if (isbottom)
		{
			animator.SetBool("ground?", true);

		}
		else
		{
			animator.SetBool("ground?", false);
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
	void Isground()
	{
		Debug.DrawRay(bottomCHK.position, Vector3.down * wallchkDistance, new Color(0, 1, 0));
		RaycastHit2D rayhit = Physics2D.Raycast(bottomCHK.position,Vector3.down * wallchkDistance, 1);
		
		/*
		if(rayhit.collider != null)
		{
			Debug.Log(rayhit.collider.name);
		}
		*/
	}

	void effect()
    {
		if (isbottom && (rigid.velocity.x > 0||rigid.velocity.x < 0))
		{
			Instantiate(Effect_smoke, new Vector2(transform.position.x, transform.position.y - 0.7f), transform.rotation);
		}
		if (iswall && direction == false && rigid.velocity.y < 0)
		{
			Instantiate(Effect_smoke, new Vector2(transform.position.x + 0.3f, transform.position.y), transform.rotation);
		}
		else if(iswall2 && direction == true && rigid.velocity.y < 0)
        {
			Instantiate(Effect_smoke, new Vector2(transform.position.x - 0.3f, transform.position.y), transform.rotation);

		}
		Invoke("effect", 0.05f);
	}
}

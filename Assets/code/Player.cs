using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float movePower = 1f;
	public float m_jumpforce = 0f;
	public int m_maxjumpcount = 0;
	float m_distance = 0f;
	public LayerMask m_layerMask = 0;
	int m_jumpCount = 0;
	public Transform wallCHk;
	public Transform wallCHk2;
	public float wallchkDistance;
	public LayerMask W_layer;
	int isRight = 1;
	public float slidingSpeed;
	bool iswall;
	bool iswall2;
	public float wallJumppower;
	public float maxspeed;

	Rigidbody2D rigid;

	Vector3 movement;
	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D>();
		m_distance = GetComponent <CapsuleCollider2D>().bounds.extents.y + 0.05f;
	}

	//Graphic & Input Updates	
	void Update()
	{
		Jump();
		CheckGround();
		iswall = Physics2D.Raycast(wallCHk.position, Vector2.right * isRight, wallchkDistance, W_layer);
		iswall2 = Physics2D.Raycast(wallCHk2.position, Vector2.right * isRight, wallchkDistance, W_layer);
	}
	private void FixedUpdate()
	{
		Move();
		if (iswall || iswall2)
		{
			//rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
			if (Input.GetAxis("Jump") != 0)
			{
				rigid.velocity = new Vector2(rigid.velocity.x, 0.9f * wallJumppower);
			}
		}
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
		rigid.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

		if (rigid.velocity.x > maxspeed) rigid.velocity = new Vector2(maxspeed,rigid.velocity.y);
		else if (rigid.velocity.x < maxspeed*(-1)) rigid.velocity = new Vector2(maxspeed*(-1),rigid.velocity.y);

	}

	void Jump()
	{
		if(Input.GetAxis("Jump") != 0)
		{
			if(m_jumpCount < m_maxjumpcount)
			{
				m_jumpCount++;
				rigid.velocity = Vector2.up * m_jumpforce;
			}
		}
	}
	void CheckGround()
	{
		if(rigid.velocity.y < 0)
		{
			RaycastHit2D t_hit = Physics2D.Raycast(transform.position, Vector2.down, 
				m_distance, m_layerMask);
			if(t_hit)
			{
				
				m_jumpCount = 0;
				
			}
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(wallCHk.position, Vector2.right * isRight * wallchkDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(wallCHk2.position, Vector2.right * isRight * wallchkDistance);
	}
}

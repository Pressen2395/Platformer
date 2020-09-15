using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Player : MonoBehaviour
{
	private Rigidbody2D rb;
	[SerializeField]
	private float speed = 0f;
	private float moveInput = 0f;
	private Vector2 characterScale;
	private float characterScaleX;
	[SerializeField]
	private float jumpForce = 0f;
	private bool isSquished = false;
	private bool isGrounded = false;
	[SerializeField]
	private Transform feetPos;
	[SerializeField] private float knockBack;
	[SerializeField]
	private float chackRadius;
	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]private LayerMask whatIsEnemy;
	private bool doubleJump;	
	private Animator animator;
	[SerializeField]
	private UIManager uiManager;
	private GameStateController gsc;	
	public static int points = 0;
	public static int health = 100;
	private enum State { idle, running, jumping, falling, damage };
	private State state;
	private bool canMove;	
	[SerializeField]
	private Transform fireBall;
	private float canFire = -1f;
	[SerializeField]
	private float fireRate;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();		
		animator = GetComponent<Animator>();
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		gsc = FindObjectOfType<GameStateController>();
		state = State.idle;
		canMove = true;
		doubleJump = true;
		characterScale = transform.localScale;
		characterScaleX = characterScale.x;
		
	}

	private void Update()
	{
		;
		if (canMove)
		{
			Move();
			isGrounded = Physics2D.OverlapCircle(feetPos.position, chackRadius, whatIsGround);
			isSquished = Physics2D.OverlapCircle(feetPos.position, chackRadius, whatIsEnemy);
			if (isGrounded)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Jump();
					doubleJump = true;
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
				{
					Jump();
					doubleJump = false;
				}
			}			
		}
		if(Input.GetKeyDown(KeyCode.Mouse0) && state != State.damage && Time.time > canFire)
		{
			Fire();
			
		}
		velocityState();
		animator.SetInteger("state", (int)state);
	}

	void Move()
	{
		moveInput = Input.GetAxis("Horizontal");
		if (moveInput > 0)
		{
			characterScale.x = characterScaleX;
		}
		else if (moveInput < 0)
		{
			characterScale.x = -characterScaleX;
		}
		transform.localScale = characterScale;
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}

	void Jump()
	{
		rb.velocity = Vector2.up * jumpForce;
		state = State.jumping;
	}

	void Fire()
	{
		canFire = Time.time + fireRate;
		Instantiate(fireBall, transform.position, Quaternion.identity);
	}

	void velocityState()
	{
		if (state == State.jumping)
		{
			if (rb.velocity.y < 0.1f)
			{
				state = State.falling;
			}
		}
		else if (state == State.falling)
		{
			if (isGrounded)
			{
				state = State.idle;
			}
		}
		else if (Mathf.Abs(rb.velocity.x) > 2f)
		{
			state = State.running;
		}
		else
		{			
			state = State.idle;
			
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Enemy" || other.collider.tag == "Obstacle")
		{
			if (!isSquished) //if enemy isnt squished
			{
				if (other.collider.tag == "Enemy")
				{
					if (other.transform.position.x > transform.position.x)
					{
						rb.velocity = new Vector2(-knockBack, rb.velocity.y);
					}
					else
					{
						rb.velocity = new Vector2(knockBack, rb.velocity.y);
					}
				}
				
				switch (other.collider.tag)
				{
					case "Enemy": Damage(20);
						break;
					case "Obstacle": Damage(50);
						break;
					default:
						break;
				}
			}
			else
			{
				Jump();
				Destroy(other.gameObject);
				AddScore(20);
			}			
		}
	}

	public void AddScore(int score)
	{
		points += score;
		uiManager.UpdateScore(points);	
	}

	public void Damage(int damage)
	{		
		canMove = false;
		health -= damage;
		animator.SetTrigger("damage");
		uiManager.UpdateHealth(health);		
		StartCoroutine(DamageCoroutine());
		if (health == 0)
		{
			animator.SetBool("isDead", true);
			Die();
		}
	}

	IEnumerator DamageCoroutine()
	{
		yield return new WaitForSeconds(0.45f);
		canMove = true;
	}


	private void Die()
	{
		Destroy(this.gameObject);
		uiManager.GameOverGameState();
		gsc.newGame = true;		
	}
}

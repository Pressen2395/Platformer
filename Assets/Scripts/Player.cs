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
	private SpriteRenderer spriteRenderer;
	private Animator animator;
	[SerializeField]
	private UIManager uiManager;
	private int points;
	private int health;
	private enum State { idle, running, jumping, falling, damage };
	private State state;
	private bool canMove;	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		points = 0;
		health = 100;
		state = State.idle;
		canMove = true;
		doubleJump = true;
	}

	private void Update()
	{
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
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Instantiate(kunai, transform.position,Quaternion.identity);
			}
		}
		velocityState();
		animator.SetInteger("state", (int)state);
	}

	void Move()
	{
		moveInput = Input.GetAxis("Horizontal");
		if (moveInput > 0)
		{
			spriteRenderer.flipX = false;
		}
		else if (moveInput < 0)
		{
			spriteRenderer.flipX = true;
		}
		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}

	void Jump()
	{
		rb.velocity = Vector2.up * jumpForce;
		state = State.jumping;
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
		if (other.collider.tag == "Enemy")
		{
			if (!isSquished)
			{
				Damage(20);
				if (other.transform.position.x > transform.position.x)
				{
					rb.velocity = new Vector2(-knockBack, rb.velocity.y);
				}
				else
				{
					rb.velocity = new Vector2(knockBack, rb.velocity.y);
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
		if(health == 0)
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
		uiManager.UpdateGameState();
	}
}

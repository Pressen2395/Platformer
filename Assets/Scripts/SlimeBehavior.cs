using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
	[SerializeField] private Transform targetA, targetB;
	[SerializeField] private float slimeSpeed = 1f;
	private bool isSwitching;
	private SpriteRenderer sprite;

	private void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	private void FixedUpdate()
	{
		if (isSwitching)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetA.position, Time.fixedDeltaTime * slimeSpeed);
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, targetB.position, Time.fixedDeltaTime * slimeSpeed);
		}
		if (transform.position == targetB.position)
		{
			isSwitching = true;
			sprite.flipX = true;
		}
		else if (transform.position == targetA.position)
		{
			isSwitching = false;
			sprite.flipX = false;
		}		
	}
	
}

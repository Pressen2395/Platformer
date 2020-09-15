using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private Transform targetA;
	[SerializeField]
	private Transform targetB;
	private bool isSwitching = false;
	void FixedUpdate()
	{		
		if (isSwitching)
		{
			//transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
			transform.position = Vector2.MoveTowards(transform.position, targetA.position, Time.deltaTime * moveSpeed);
		}
		else
		{
			//transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
			transform.position = Vector2.MoveTowards(transform.position, targetB.position, Time.deltaTime * moveSpeed);
		}
		if (transform.position.x == targetB.position.x)
		{
			isSwitching = true;			
		}
		else if (transform.position.x == targetA.position.x)
		{
			isSwitching = false;
		}
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.name == "Player")
		{
			other.transform.parent = this.transform;
		}
	}
	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.name == "Player")
		{
			other.transform.parent = null;
		}
	}
}

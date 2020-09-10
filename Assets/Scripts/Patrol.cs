using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private float rayDistance;
	private bool movingLeft;
	[SerializeField]
	private Transform groundDetection;

	private void Update()
	{
		transform.Translate(Vector2.right * speed * Time.deltaTime);

		RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance);
		if(groundInfo.collider == false)
		{
			if(movingLeft == true)
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
				movingLeft = false;
			} else
			{
				transform.eulerAngles = new Vector3(0, -180, 0);
				movingLeft = true;
			}
		}
	}
}

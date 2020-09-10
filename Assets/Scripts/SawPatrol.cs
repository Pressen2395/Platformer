using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawPatrol : MonoBehaviour
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

		
	}
}





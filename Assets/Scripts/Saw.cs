using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _rotationSpeed = -350f;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private Transform targetA;
	[SerializeField]
	private Transform targetB;
	private bool isSwitching = false;
	// Update is called once per frame	
	void Update()
    {		
		transform.Rotate(new Vector3(0, 0, (1 * _rotationSpeed * Time.deltaTime))) ;	
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
			_rotationSpeed *= -1;
		}
		else if (transform.position.x == targetA.position.x)
		{
			isSwitching = false;
			_rotationSpeed *= -1;
		}
	}
}

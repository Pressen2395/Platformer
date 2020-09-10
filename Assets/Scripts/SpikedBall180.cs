using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall180 : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float maxLeftSwing;
    [SerializeField]
    private float maxRightSwing;
    [SerializeField]
    private float maxSwingVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = maxSwingVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
    }
    void Swing()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < maxRightSwing
            && rb.angularVelocity > 0 && rb.angularVelocity < maxSwingVelocity)
        {
            rb.angularVelocity = maxSwingVelocity;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > maxLeftSwing
            && rb.angularVelocity < 0 && rb.angularVelocity > maxSwingVelocity)
		{
            rb.angularVelocity = maxSwingVelocity * -1;
		}
	}
}

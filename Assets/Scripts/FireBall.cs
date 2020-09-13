using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0f;
    private Player player;
    private Rigidbody2D rb;
    private float fireBallDirection;
    private Vector2 playerDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();

        if(player.transform.localScale.x > 0)
            rb.velocity = transform.right * _speed;
        else
            rb.velocity = (transform.right * -1) * _speed;
    }

    // Update is called once per frame
    void Update()
    {        
        StartCoroutine(FireBallCoroutine());
    }
	IEnumerator FireBallCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            player.AddScore(50);
		}
        else if (other.CompareTag("Ground") || other.CompareTag("Border"))
		{
            Destroy(this.gameObject);
		}
	}
}

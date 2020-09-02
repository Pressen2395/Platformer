using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 2f;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Player not found");
        }  
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Enemy")
		{
            Destroy(this.gameObject);
            player.AddScore(20);
            Destroy(other.gameObject);
		}
        else if(other.tag == "Ground" || other.tag == "Border")
		{
            Destroy(this.gameObject);
		}
	}

    
}

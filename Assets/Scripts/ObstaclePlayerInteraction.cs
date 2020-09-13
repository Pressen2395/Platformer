using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlayerInteraction : MonoBehaviour
{
	private Player player;
	private void Start()
	{
		player = FindObjectOfType<Player>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (player == null)
		{
			Debug.LogError(message: "Player GO not found - SpikedBall.cs");
		}
		if (other.CompareTag("Player"))
		{
			player.Damage(30);
		}

	}
}

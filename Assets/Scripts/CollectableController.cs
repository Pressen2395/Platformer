using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
	private int score;
	private Animator anim;
	private void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player == null)
			{
				Debug.LogError("Player Object NULL : CollectableController.cs");
				return;
			}						
			anim.SetTrigger("isPickedUp");
			StartCoroutine(DestroyCollectableRoutine());
			switch (this.name)
			{
				case "Apple": score = 5;					
					break;
				case "Orange": score = 6;					
					break;
				case "Melon": score = 7;
						break;
				default:
					break;
			}
			player.AddScore(score);
		}
	}
	IEnumerator DestroyCollectableRoutine()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Text healthText;
	[SerializeField] private Text gameOverText;
	[SerializeField] private Text restartText;
	[SerializeField] private Text levelText;
	private Scene scene;
	private void Awake()
	{
		UpdateScore(Player.points);
		UpdateHealth(Player.health);	
	}
	private void Start()
	{	
		gameOverText.gameObject.SetActive(false);
		restartText.gameObject.SetActive(false);		
		scene = SceneManager.GetActiveScene();
		levelText.text = scene.name;			
	}
	

	public void UpdateScore(int score)
	{
		scoreText.text = "Score: " + score.ToString("0000"); //Score: 0000
	}
	public void UpdateHealth(int health)
	{		
		healthText.text = "Health: " + health;
	}
	public void GameOverGameState()
	{
		gameOverText.gameObject.SetActive(true);
		restartText.gameObject.SetActive(true);
		StartCoroutine(GameOverFlickerRoutine());
	}	
	IEnumerator GameOverFlickerRoutine()
	{
		while (true)
		{
			gameOverText.text = "Game Over";
				yield return new WaitForSeconds(0.5f);
			gameOverText.text = "";
				yield return new WaitForSeconds(0.5f);
		}
	}	
}

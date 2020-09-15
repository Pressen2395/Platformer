using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateController : MonoBehaviour
{
    private GameObject collectables;
    public bool newGame;
    // Start is called before the first frame update  
    void Start()
    {
        collectables = GameObject.FindGameObjectWithTag("Collectables");
    }
    // Update is called once per frame
    void Update()
    {        
        if (collectables)
		{
            if(collectables.transform.childCount == 0)
			{
                LevelComplete();
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
            Player.points = 0;
            Player.health = 100;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
    }

    public void LevelComplete()
    {
        //SceneManager.GetActiveScene().buildIndex + 1   
        if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
		{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
		{
            GameComplete();
		}               
    }
    public void GameComplete()
	{
        //GO to main Menu
        Debug.Log("Go to Main Menu");
	}
}

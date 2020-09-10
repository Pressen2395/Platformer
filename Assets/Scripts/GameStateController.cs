using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateController : MonoBehaviour
{
    private GameObject collectables;
    private UIManager uiManager;
    private Scene scene;
    private bool newGame = true;
    // Start is called before the first frame update
    void Start()
    {
        collectables = GameObject.FindGameObjectWithTag("Collectables");
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();              
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
    }

    public void LevelComplete()
    {
        //SceneManager.GetActiveScene().buildIndex + 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

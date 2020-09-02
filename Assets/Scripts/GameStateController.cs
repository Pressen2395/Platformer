using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateController : MonoBehaviour
{
    [SerializeField]private CollectableController collectable;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        collectable = GetComponent<CollectableController>();
    }

    // Update is called once per frame
    void Update()
    {
        parent = GameObject.FindWithTag("Collectables");
        if(parent == null)
		{
            Debug.LogError("Could not find GO");
		} 
        if(parent.transform.childCount == 0)
        {
            Debug.Log("Proceed to next level");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
            SceneManager.LoadScene(0);
		}
    }
}

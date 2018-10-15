using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
    public int lastOpenLevelIndex = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenShop()
    {

    }

    public void OpenLeaderboard()
    {

    }

    public void MultiPlayer()
    {

    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene(lastOpenLevelIndex);
    }
}

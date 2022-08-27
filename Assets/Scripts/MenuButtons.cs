using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {


    public GameObject firstMenu;
    public GameObject levelMenu;
    public GameObject howToMenu;
    

    // Use this for initialization
    void Start () {
        firstMenu.gameObject.SetActive(true);
        howToMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void options()
    {

    }


    //Voids for the menu buttons
    public void back()
    {
        firstMenu.gameObject.SetActive(true);
        howToMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(false);
    }


    public void startGame()
    {
        firstMenu.gameObject.SetActive(false);
        levelMenu.gameObject.SetActive(true);
    }

    public void normalGame()
    {
        SceneManager.LoadScene("LevelTetris");
    }

    public void expertGame()
    {
        SceneManager.LoadScene("LevelExpert");
    }


    public void howToPlay()
    {
        firstMenu.gameObject.SetActive(false);
        howToMenu.gameObject.SetActive(true);
    }

    public void exit()
    {
        Application.Quit();
    }
}

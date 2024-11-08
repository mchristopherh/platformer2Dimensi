using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public string mainMenu;
    private void Awake()
    {
        instance = this;
        Debug.Log("UIController instance initialized.");
    }

    public Image[] heartIcons;
    public Sprite heartFull, heartEmpty;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay(int health,  int maxHealth)

    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            /*if (health <= i)
            {
                heartIcons[i].enabled = false;
            }*/

            if (health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;

                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }

        }
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart(){
        Debug.Log("Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu(){
        Debug.Log("Go to Main Menu");
        SceneManager.LoadScene(mainMenu);
    }

    public void ShowVictory()
    {
        victoryScreen.SetActive(true);
    }
}

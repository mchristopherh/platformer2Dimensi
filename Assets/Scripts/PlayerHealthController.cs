using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    
    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;
    public float invincibilityLength = 1f;
    private float invincibilityCounter;
    public SpriteRenderer theSR;
    public Color normalColor, fadeColor;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if(invincibilityCounter <= 0)
            {
                theSR.color = normalColor;
            }
        }
    }

    public void DamagePlayer()
    {
        if(invincibilityCounter <= 0)
        {
            currentHealth--;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(GameOverCo());

                // gameObject.SetActive(false);
            } else{
                invincibilityCounter = invincibilityLength;
                theSR.color = fadeColor;
                thePlayer.Knockback();
            }

            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }

    public void AddHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(0.001f);
        if (UIController.instance != null){
            UIController.instance.ShowGameOver();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class CharacterStats : MonoBehaviour
{
    public int damage = 10;
    public int health = 100;
    public GameObject gameOver;
    public GameObject escape;
    public GameObject HUD;
    public Text healthUI;
    float gameOverTimer = 6f;
    AudioSource audioSource;
    public AudioClip block;
    public float blockCooldown = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            blockCooldown = 1f;
        }

        blockCooldown -= Time.deltaTime;

        if (gameOverTimer >= 3f && gameOverTimer <=5f)
        {
            gameOver.SetActive(false);
            gameObject.GetComponent<PlayerController>().unsetEscape();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        gameOverTimer += Time.deltaTime;
        healthUI.text = "Health: " + health.ToString();

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
            Debug.Log(health);
        }

        if (Input.GetKeyDown("`"))
        {
            damage = 9999;
            health = 9999;
        }

        if (Input.GetKeyDown("e"))
        {
            if (Inventory.potions != 0 && health < 100)
            {
                if (health + 20 > 100)
                {
                    int hp = health + 20;
                    int hp2 = hp - 100;
                    int hp3 = 20 - hp2;
                    health = health + hp3;
                    Debug.Log(health);
                    Inventory.potions--;
                }
                else
                {
                    health = health + 20;
                    Inventory.potions--;
                }                
                Debug.Log(health);                
            }
        }
    }

    public void TakeDamage(int damageIn)
    {
        Debug.Log(blockCooldown);
        if (blockCooldown >= 0)
        {
            audioSource.clip = block;
            audioSource.Play();
            health -= (damageIn / 2);
        }
        else 
        {
            health -= damageIn;
        }
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("died");
        gameOver.SetActive(true);
        gameObject.GetComponent<PlayerController>().setEscape();
        gameOverTimer = 0f;
    }

    public float getBlock()
    {
        return blockCooldown;
    }
}

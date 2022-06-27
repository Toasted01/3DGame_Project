using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class Inventory : MonoBehaviour
{
    public static int potions = 4;
    public static int orb1 = 0;
    public static int orb2 = 0;
    public static int orb3 = 0;
    public bool heroic = false;

    public Text PotionCount;
    public Text Orb1Count;
    public Text Orb2Count;
    public Text Orb3Count;
    public GameObject BlindWellExit;
    public GameObject Sphere1;
    public GameObject Sphere2;
    public GameObject Sphere3;
    public GameObject escape;
    public GameObject HUD;
    public GameObject Congrats;
    PlayerController controller;
    float gameOverTimer = 6f;

    // Update is called once per frame
    void Update()
    {
        gameOverTimer += Time.deltaTime;

        if (gameOverTimer >= 3f && gameOverTimer <= 5f)
        {
            Congrats.SetActive(false);
            gameObject.GetComponent<PlayerController>().unsetEscape();
            HUD.SetActive(false);
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown("o"))
        {
            orb1 = 1;
            orb2 = 1;
            orb3 = 1;
        }
        if (heroic)
        {
            potions = 0;
        }

        PotionCount.text = potions.ToString();
        Orb1Count.text = orb1.ToString();
        Orb2Count.text = orb2.ToString();
        Orb3Count.text = orb3.ToString();
    }

    void PotionPickup()
    {
        potions++;
    }

    void Boss1Defeat()
    {
        orb1 = 1;
        potions = potions + 2;
    }

    void Boss2Defeat()
    {
        orb2 = 1;
        potions = potions + 2;
    }

    void Boss3Defeat()
    {
        BlindWellExit.SetActive(true);
        orb3 = 1;
        potions = potions + 2;
    }

    void EndGame()
    {
        if (orb1 == 1)
        {
            Sphere1.SetActive(true);
        }
        if (orb2 == 1)
        {
            Sphere2.SetActive(true);
        }
        if (orb3 == 1)
        {
            Sphere3.SetActive(true);
        }

        if (orb1 == 1 && orb2 == 1 && orb3 == 1)
        {
            Congrats.SetActive(true);
            gameObject.GetComponent<PlayerController>().setEscape();
            HUD.SetActive(false);
            gameOverTimer = 0.0f;            
        }
    }
}

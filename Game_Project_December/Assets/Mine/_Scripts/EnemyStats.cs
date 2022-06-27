using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int damage = 10;
    public int health = 100;
    public Text healthUI;

    PlayerManager playerManager;
    Transform playerTransform;

    public void Start()
    {
        playerTransform = PlayerManager.instance.player.transform;
    }

    public void TakeDamage(int damageIn, int modifier)
    {
        health -= (damageIn / modifier);
        Debug.Log(health);
        if (health <= 0)
        {
            healthUI.text = "";
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died");
        switch (gameObject.tag)
        {
            case "Boss1":
                playerTransform.SendMessage("Boss1Defeat");
                break;

            case "Boss2":
                playerTransform.SendMessage("Boss2Defeat");
                break;

            case "Boss3":
                playerTransform.SendMessage("Boss3Defeat");
                break;
        }
        healthUI.text = "";
        Destroy(gameObject);

    }

    public void ShowEnemy()
    {
        healthUI.text = " " + health.ToString();
    }
}

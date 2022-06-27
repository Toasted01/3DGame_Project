using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class Enemy : MonoBehaviour
{
    PlayerManager playerManager;
    EnemyStats enemyStats;
    GameObject player;
    Transform playerTransform;

    public float radius = 3f;

    bool isFocus = false;
    bool hasInteracted = false;

    private void Start()
    {
        player = PlayerManager.instance.player;
        playerTransform = player.transform;
        enemyStats = GetComponent<EnemyStats>();
    }

    public void EnemyInteracting()
    {
        Debug.Log("!");
        Combat playerCombat = player.GetComponent<Combat>();
        if (playerCombat != null)
        {
            playerCombat.AttackEnemy(enemyStats);
        }
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= radius)
            {
                EnemyInteracting();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform1)
    {
        isFocus = true;
        playerTransform = playerTransform1;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
}

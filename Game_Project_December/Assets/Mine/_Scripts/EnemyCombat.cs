using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyCombat : MonoBehaviour
{
    EnemyStats myStats;
    AudioSource audioSource;
    public AudioClip hit;

    private float attackCooldown = 0f;

    private void Start()
    {
        myStats = GetComponent<EnemyStats>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void AttackPlayer(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            targetStats.TakeDamage(myStats.damage);
            PlayEnemyHit();
            attackCooldown = 1f;

        }
    }

    public void PlayEnemyHit()
    {
        audioSource.clip = hit;
        audioSource.Play();
    }
}

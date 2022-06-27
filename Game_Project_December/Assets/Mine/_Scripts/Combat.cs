using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Combat : MonoBehaviour
{
    CharacterStats myStats;
    AudioSource audioSource;
    public AudioClip hit;

    private float attackCooldown = 0f;
    public bool blueFlame = false;
    public float blueFlameTimer = 0f;
    public GameObject sword;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        sword.SetActive(false);
        attackCooldown -= Time.deltaTime;
        blueFlameTimer -= Time.deltaTime;

        if (blueFlameTimer <= 0f)
        {
            blueFlame = false;
        }
        else 
        {
            blueFlame = true;
        }
    }

    void BlueFlame()
    {
        blueFlameTimer = 5f;
    }

    void DoDamage(EnemyStats targetStats)
    {
        bool block = false;
        float blockTimer = gameObject.GetComponent<CharacterStats>().getBlock();
        sword.SetActive(true);
        if (blockTimer >= 0f)
        {
            block = true;
        }
        if (block)
        {
            targetStats.TakeDamage(myStats.damage,2);
        }
        else
        {
            targetStats.TakeDamage(myStats.damage,1);
        }
        attackCooldown = 0.5f;
        PlayHit();
    }

    public void AttackEnemy(EnemyStats targetStats)
    {
        if (attackCooldown <= 0f)
        {

            if (targetStats.tag == "Boss2" && blueFlame)
            {
                DoDamage(targetStats);
            }
            else if (targetStats.tag == "Boss2" && !blueFlame)
            {

            }
            else
            {
                DoDamage(targetStats);
            }
            
        }
    }

    public void PlayHit()
    {
        audioSource.clip = hit;
        audioSource.Play();
    }
}

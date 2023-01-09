using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bossState
{
    SpikeAttack,
    DashAttack,
}
public class BossBrain : MonoBehaviour
{
    public bossState state;
    [Header("Attacks")]
    public DashAttack dashATK;
    public spikeAttack spikeATK;
    

    [Header("Values")]
    public int randomATK;
    public float resetTime;
    public float TimeBetweenAttacks;

    

    public void Update()
    {
        if(TimeBetweenAttacks > 0)
        {
            TimeBetweenAttacks = TimeBetweenAttacks - 1 * Time.deltaTime;
        }
        else if(0 > TimeBetweenAttacks)
        {
            TimeBetweenAttacks = resetTime;
            ChooseAttack();
        }
    }

    public void ChooseAttack()
    {
        randomATK = Random.Range(1, 3);
        if(randomATK == 1)
        {
            state = bossState.DashAttack;
        }
        if(randomATK == 2)
        {
            state = bossState.SpikeAttack;
        }
        Attack();
    }
    public void Attack()
    {
        if(state == bossState.DashAttack)
        {
            dashATK.spawnPoints();
        }
        if (state == bossState.SpikeAttack)
        {
            spikeATK.CallSpike();
        }
    }
}

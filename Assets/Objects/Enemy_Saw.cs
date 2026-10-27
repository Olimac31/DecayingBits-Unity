using System;
using UnityEngine;

public class Enemy_Saw : Enemy
{

    public bool moves = false;
    public float saw_speed = 350f;
    public Transform[] moveSpots;
    int spotIndex = 0;
    public bool inReverse = false;

    Animator anims;

    protected override void Start()
    {
        base.Start();
        
        MAX_SPEED = 0f;
        has_gravity = false;
        stompable = false;
        ignore_damage = true;
        cannot_damage = false;
        instakill = false;

        anims = GetComponent<Animator>();
        anims.speed = 0.2f;

        if(moves)
        {
            change_state("moving");
        }
        else
        {
            change_state("default");
        }
    }

    protected override void state_enters()
    {
        switch(state)
        {
            case "default":
            state_default_enter();
            break;

            case "moving":
            state_moving_enter();
            break;

            default:
            break;
        }
    }

    protected override void state_behaviors()
    {
        switch(state)
        {
            case "default":
            state_default_behavior();
            break;

            case "moving":
            state_moving_behavior();
            break;

            default:
            break;
        }
    }

    void state_moving_enter()
    {
        
    }

    void state_moving_behavior()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[spotIndex].position, saw_speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[spotIndex].position) < 0.2f)
        {
            if(spotIndex < moveSpots.Length - 1)
            {
                spotIndex++;
            }
            else if(spotIndex >= moveSpots.Length - 1)
            {
                spotIndex = 0;
                if(inReverse)
                {
                    Array.Reverse(moveSpots);
                }
            }
        }
    }
}

using UnityEngine;

public class Enemy_Basic : Enemy
{
    public GameObject eyeLeft, eyeRight;
    protected override void Start()
    {
        base.Start();
        
        MAX_SPEED = 8f;
        has_gravity = true;
        stompable = true;
        ignore_damage = false;
        cannot_damage = false;

        change_state("walk_left");
    }

    protected override void state_enters()
    {
        switch(state)
        {
            case "walk_left":
            state_walk_left_enter();
            break;

            case "walk_right":
            state_walk_right_enter();
            break;

            default:
            break;
        }
    }

    protected override void state_behaviors()
    {
        switch(state)
        {
            case "walk_left":
            state_walk_left_behavior();
            break;

            case "walk_right":
            state_walk_right_behavior();
            break;

            default:
            break;
        }
    }

    void state_walk_left_enter()
    {
        
    }
    void state_walk_left_behavior()
    {
        velocity.x = -MAX_SPEED;

        if (left_is_colliding())
        {
            change_state("walk_right");
        }
    }

    void state_walk_right_enter()
    {
        
    }
    void state_walk_right_behavior()
    {
        velocity.x = MAX_SPEED;

        if (right_is_colliding())
        {
            change_state("walk_left");
        }
    }

    bool left_is_colliding()
    {
        return eyeLeft.GetComponent<detects_collision>().is_colliding();
    }
    bool right_is_colliding()
    {
        return eyeRight.GetComponent<detects_collision>().is_colliding();
    }
}

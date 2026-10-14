using UnityEngine;

public class EnemyDeathblock : Enemy
{
    protected override void Start()
    {
        base.Start();
        
        MAX_SPEED = 0f;
        has_gravity = false;
        stompable = false;
        ignore_damage = true;
        cannot_damage = false;
        instakill = true;

        GetComponent<SpriteRenderer>().enabled = false;

        change_state("default");
    }

}

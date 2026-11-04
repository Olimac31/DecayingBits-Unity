using UnityEngine;

public class EnemyDeathblock : Enemy
{
    public bool is_instakill = true;
    protected override void Start()
    {
        base.Start();
        
        MAX_SPEED = 0f;
        has_gravity = false;
        stompable = false;
        ignore_damage = true;
        cannot_damage = false;
        instakill = is_instakill;

        GetComponent<SpriteRenderer>().enabled = false;

        change_state("default");
    }

}

using UnityEngine;

public class detects_enemyeffector : detects_collision
{
    public Collider2D last_other;
    public override void enter_checks(Collider2D other)
    {
        if(other.tag == "EnemyEffector")
        {
            colliding = true;
            last_other = other;
        }
    }
}

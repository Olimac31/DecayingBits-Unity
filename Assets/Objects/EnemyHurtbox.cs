using UnityEngine;

public class EnemyHurtbox : detects_player
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Player")
        {
            GetComponentInParent<Enemy>().on_hurtbox_entered();
            Debug.Log("EnemyHurtbox: Player collided with enemy");
        }
    }
}

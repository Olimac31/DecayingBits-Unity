using UnityEngine;

public class EnemyHead : detects_player
{
    public float stomp_damage = 1.0f;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Player")
        {
            GetComponentInParent<Enemy>().head_stomped(stomp_damage);
        }
    }
}

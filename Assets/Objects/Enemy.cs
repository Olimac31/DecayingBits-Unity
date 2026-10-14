using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    protected float base_damage = 1f; // Damage
    protected bool stompable = true; // Can be stomped on
    protected bool ignore_damage = false; // Can't be damaged
    protected bool cannot_damage = false; // Can't damage
    protected bool instakill = false;

    public float starting_hp_multiplier = 1.0f;

    // Physics values
    protected float MULTIPLIER = 10f;
    protected bool has_gravity = false;
    protected float GRAVITY = 120.0f;
    protected float WALK_SPEED = 3.5f;
    protected float JUMP_SPEED = 65f;
    protected float MAX_SPEED = 26f;
    protected float player_impulse = 30f;
    protected Vector2 velocity = new Vector2(0, 0);
    
    // Colliders
    public GameObject colFloor; // Floor collider
    public GameObject colHurtbox; // Enemy hurts with this
    public GameObject colHead;  // Enemy is stomped on this
    public Rigidbody2D rbody;

    protected Vector2 startPos = new Vector2(0, 0);

    public bool friendly = false;

    protected float max_health = 5f;
    protected float health = 5f;

    protected string state = "default";

    public UnityEvent stomped;
    public UnityEvent killed;

    //[SerializeField] protected GameObject deathEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        max_health *= starting_hp_multiplier;
        health = max_health;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        rbody.gravityScale = 0;
        
        if(health <= 0)
        {
            killed.Invoke();

            //Instantiate(deathEffectPrefab, transform.position, transform.rotation);

            event_on_death();
            Destroy(gameObject);
        }

        state_behaviors();
    }
    

    protected virtual void FixedUpdate()
    {
        if (has_gravity)
        {
            gravity_behavior();
        }

        // Apply velocity
        rbody.linearVelocity = new Vector2(velocity.x, velocity.y);
    }

    protected virtual void event_on_damage()
    {
        
    }
    protected virtual void event_on_death()
    {
        
    }

    protected virtual void change_state(string new_state = "default")
    {
        state = new_state;
        state_enters();
    }

    protected virtual void state_enters()
    {
        switch(state)
        {
            case "default":
            state_default_enter();
            break;

            default:
            break;
        }
    }
    protected virtual void state_behaviors()
    {
        switch(state)
        {
            case "default":
            break;

            default:
            break;
        }
    }

    public virtual void state_default_enter()
    {
        
    }
    public virtual void state_default_behavior()
    {
        
    }

    public virtual bool damage_checks()
    {
        if (health > 0 && !friendly && cannot_damage && custom_damage_checks())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool custom_damage_checks()
    {
        return true;
    }

    protected bool bolGround()
    {
        return colFloor.GetComponent<detects_collision>().is_colliding();
    }

    public void gravity_behavior()
    {
        if (!bolGround())
        {
            velocity.y -= GRAVITY * Time.deltaTime;
        }
        else
        {
            reset_variables_on_ground();
        }
    }

    public virtual void reset_variables_on_ground()
    {
        if (velocity.y < 0)
        {
            velocity.y = 0;
        }
    }

    // When player enters the hurtbox -> HURT HIM!!!
    public void on_hurtbox_entered()
    {
        // Obtain player gameObject from its collider
        playerMovement the_player;
        the_player = GameObject.Find("obj_Player").GetComponent<playerMovement>();

        if(the_player.invincible <= 0)
        {
            the_player.takeDamage(base_damage);
        }

        if(instakill)
        {
            the_player.takeDamage(9999, true);
        }
    }

    // When player stomps enemy -> take damage :(
    public void head_stomped(float damage_to_take = 1.0f)
    {
        // Obtain player gameObject from its collider
        playerMovement the_player;
        the_player = GameObject.Find("obj_Player").GetComponent<playerMovement>();

        if (the_player.velocity.y < 0 && stompable)
        {
            take_damage(damage_to_take);
            the_player.velocity.y = player_impulse;
        }

        stomped.Invoke();
    }

    public void take_damage(float amount)
    {
        if (!ignore_damage)
        {
            health -= amount;
            event_on_damage();
        }
    }
}

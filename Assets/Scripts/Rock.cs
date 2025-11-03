using UnityEngine;
using UnityEngine.Rendering;

public class Rock : Weapon
{
    public Rigidbody2D rb;
    public Vector2 force;
    public override void Move()
    {
        rb.AddForce(force);
    }

    public override void OnHitWith(Character obj)
    {
        if (obj is Player)
            obj.TakeDamage(this.damage);
    }

        

       
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 200;
        force = new Vector2(GetShootDirection() * 90, 400);
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

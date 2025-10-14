using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //attributes
    private int health;
    public int Health 
    { 
      get => health; 
      set => health = (value < 0) ? 0 : value; 
    }

    protected Animator anim;
    protected Rigidbody2D rb;


    //methods
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{this.name} took {damage} damage, Current health: {Health}");
    }
    public bool IsDead()
    {
        if (Health <= 0)
        {
            Debug.Log($"{this.name} is dead.");
            Destroy(this.gameObject);
            return true;
        }
        else return false;
        
    }





    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

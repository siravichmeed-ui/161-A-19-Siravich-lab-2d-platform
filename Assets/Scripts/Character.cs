using System;
using System.Runtime;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //attributes
    [SerializeField] private int maxHealth;

    private int health;
    public int MaxHealth => maxHealth;

    public int Health 
    { 
      get => health;
        private set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(health, maxHealth);   // แจ้ง HPBar ทุกครั้งที่เปลี่ยน
        }
    }
    public event Action<int, int> OnHealthChanged;
    protected Animator anim;
    protected Rigidbody2D rb;

    public void Initialize(int startHealth)
    {

        Health = startHealth;
        maxHealth = startHealth;
        
        Debug.Log($"{this.name} initialized with {Health} health.");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //methods
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{this.name} took {damage} damage, Current health: {Health}");
        IsDead();
        
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

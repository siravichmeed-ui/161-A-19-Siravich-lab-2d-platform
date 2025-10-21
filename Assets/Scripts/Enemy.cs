using UnityEngine;

public abstract class Enemy : Character
{

    public abstract void Behavior();
    public int DamageHit { get; protected set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class Player : Character , IShootable
{
    [SerializeField] private PlayerHPBar hpBar;
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReloadTime = 1.0f;
        WaitTime = 0f;
        base.Initialize(100);
        if (hpBar != null) hpBar.Bind(this);
    }
    public void OnHitWith(Enemy enemy)
    {
        TakeDamage(enemy.DamageHit);


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log($"{this.name} Hit with {enemy.name}!");
            OnHitWith(enemy);
        }
    }
    private void FixedUpdate()
    {
        WaitTime +=Time.fixedDeltaTime;
    }
    // Update is called once per frame
    private void Update()
    {
       Shoot();
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            Banana banana = bullet.GetComponent<Banana>();
            if (banana != null)
                banana.InitWeapon(20, this);
                
            WaitTime = 0f;
        }
    }
}

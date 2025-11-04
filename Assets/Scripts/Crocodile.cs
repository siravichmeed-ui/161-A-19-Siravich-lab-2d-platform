using UnityEngine;

public  class Crocodile : Enemy, IShootable
{
    [SerializeField] private float atkRange;
    public Player player; //target to atk
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }
    void Start()
    {
        base.Initialize(100);
        DamageHit = 30;
        //set atk range and target
        atkRange = 20.0f;
        player = GameObject.FindFirstObjectByType<Player>();
        WaitTime = 0.0f;
        ReloadTime = 2.0f;
    }

    private void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
        Behavior();
    }
    public override void Behavior()
    {
        //find distance between Croccodile and Player
        Vector2 distance = transform.position - player.transform.position;
        if (distance.magnitude <= atkRange)
        {
            Debug.Log($"{player.name} is in the {this.name}'s atk range!");
            Shoot();
        }
    }
    public void Shoot()
    {
       if (WaitTime >= ReloadTime)
        {
            anim.SetTrigger("Shoot");
            var bullet = Instantiate(Bullet, ShootPoint.position,  Quaternion.identity);
            Rock rock = bullet.GetComponent<Rock>();
            rock.InitWeapon(20, this);
            WaitTime = 0f;
        }
    }
}

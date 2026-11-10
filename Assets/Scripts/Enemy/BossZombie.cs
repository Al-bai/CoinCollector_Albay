using UnityEngine;

public class BossZombie : Enemy
{
    [SerializeField] private ZombieConfig config;

    protected override void Start()
    {
        base.Start();

        Debug.Log("Boss HP: " + config.hp);
        Debug.Log("Boss Kecepatan: " + config.kecepatan);
    }

    public override void Serang()
    {
        base.Serang();
    }
}
using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.9f;
    [SerializeField] private float jedaSerang = 3f;

    [SerializeField] private UnityEvent onZombieMatiVisual;

    // 'static' -> SATU event ini dipakai bersama oleh SEMUA zombie.
    // <Enemy> -> saat dipicu, ia mengirim info "zombie mana yang mati".
    public static event Action<Enemy> OnZombieMati;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    public float ms = 2f;
    protected Transform player;
    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();

        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    void PerilakuIdle()
    {
        transform.Rotate(0, 0, 30f * Time.deltaTime);
        //Debug.Log("ENEMY SEDANG IDLE");
    }

    void PerilakuPatrol()
    {
        float gerakan = Mathf.Sin(Time.time * 2f) * ms * Time.deltaTime;

        transform.position += new Vector3(gerakan, 0, 0);

        //Debug.Log("ENEMY SEDANG PATROLI");
    }

    void PerilakuChase()
    {
        Kejar();
        //Debug.Log("ENEMY SEDANG MENGEJAR PLAYER");
    }

    void PerilakuAttack()
    {
        if (player == null) return;

        Vector2 arah = player.position - transform.position;

        if (arah != Vector2.zero)
        {
            float sudut = Mathf.Atan2(arah.y, arah.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, sudut);
        }

        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
        //Debug.Log("ENEMY SEDANG ATTACK");
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);

    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak  <= jarakSerang)
            state = StateZombie.ATTACK;
        else if (jarak <= jarakDeteksi)
            state = StateZombie.CHASE;
        else if (jarak > 10f)
            state = StateZombie.IDLE;
        else
            state = StateZombie.PATROL;   
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    public void KenaDamage(int jumlah) 
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {

        Debug.Log(name + " kalah!");
        // '?.Invoke' -> aman walau belum ada yang subscribe (tidak error)
        OnZombieMati?.Invoke(this); // kirim 'this' = info diri sendiri
        // di dalam Mati():
        onZombieMatiVisual?.Invoke();
        Destroy(gameObject);
    }

    

}

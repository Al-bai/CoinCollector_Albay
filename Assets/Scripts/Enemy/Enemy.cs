using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;

    [Header("Pengaturan Serangan")]
    [SerializeField] private int damageKePlayer = 15;

    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.9f;
    [SerializeField] private float jedaSerang = 3f;

    [SerializeField] private UnityEvent onZombieMatiVisual;

    [Header("Input Animasi")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // 'static' -> SATU event ini dipakai bersama oleh SEMUA zombie.
    // <Enemy> -> saat dipicu, ia mengirim info "zombie mana yang mati".
    public static event Action<Enemy> OnZombieMati;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    public float ms = 2f;
    protected Transform player;

    private float timerIdle; //untuk idle

    protected virtual void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();
        AturAnimasiDanArah();

        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    protected virtual void AturAnimasiDanArah()
    {
        if (anim != null)
        {
            bool isMoving = (state == StateZombie.CHASE || state == StateZombie.PATROL);
            
        }

     
        if (spriteRenderer != null && player != null)
        {
            
            if (state == StateZombie.CHASE || state == StateZombie.ATTACK)
            {
                float bedaX = player.position.x - transform.position.x;
                
                
                if (bedaX < 0)
                {
                    spriteRenderer.flipX = true;
                }
               
                else if (bedaX > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    void PerilakuIdle()
    {
       timerIdle -= Time.deltaTime;
      
        if (timerIdle <= 0f)
        {
            if (spriteRenderer != null)
            {
                
                spriteRenderer.flipX = !spriteRenderer.flipX; 
            }
                timerIdle = UnityEngine.Random.Range(1f, 3f);
        }
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
        if (player != null)
        {
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
               
                playerHealth.TakeDamage(damageKePlayer);
                Debug.Log($"{gameObject.name} menyerang player dan memberikan {damageKePlayer} damage!");
            }
        }

        Mati();
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

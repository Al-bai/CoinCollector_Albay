using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System


public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    // public int skor = 0;
    public float kecepatan = 5f;
    
    private Vector2 arahGerak; // nilai dari action "Move"

    private Animator animator;

    void Start()
    {
       // Mengambil komponen Animator dari GameObject ini
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }
    // Dipanggil OTOMATIS oleh komponen Player Input
    // saat action "Move" pada asset InputSystem_Actions aktif.
    // Nama method WAJIB: On + nama action -> OnMove
    void OnMove(InputValue value)
    {
    // TODO: ambil nilai Vector2 dari input, simpan ke arahGerak
    arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
    
  
    Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
    transform.position += arah * kecepatan * Time.deltaTime;

    if (arahGerak != Vector2.zero)
        {
            // Mengirim nilai arah x dan y ke parameter Animator
            animator.SetFloat("MoveX", arahGerak.x);
            animator.SetFloat("MoveY", arahGerak.y);
            
            // Mengaktifkan parameter isMoving (berguna jika kamu pakai transisi Idle -> Move)
            animator.SetBool("isMoving", true); 
        }
        else
        {
            // Matikan isMoving jika player diam
            animator.SetBool("isMoving", false);
        }
   
    }

    // Dipanggil otomatis saat Player menyentuh objek ber-Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: cek apakah yang disentuh punya tag "Coin"
        if (other.CompareTag("Coin"))
        {
            // TODO: hancurkan koin yang tersentuh
            Destroy(other.gameObject);
            gameManager.AmbilKoin();
            // // TODO: tambah skor sebanyak 1
            // skor++;
            // // TODO: tampilkan skor ke Console
            // Debug.Log( "Score : " + skor );

        }
    
    }

   
}

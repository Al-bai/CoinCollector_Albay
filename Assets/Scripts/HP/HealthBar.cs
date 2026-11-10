using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthSlider;     // Bar darah depan
    public Slider easeSlider;       // Bar belakang (Damage Chunk)
    public Image healthFill;        // Gambar fill pada healthSlider
    public Image easeFill;          // Gambar fill pada easeSlider (opsional, jika ingin ubah warna via script)

    [Header("Color Settings")]
    public Gradient healthGradient; // Gradien darah (Hijau -> Kuning -> Merah)
    public Color damageChunkColor = new Color(1f, 0.6f, 0.8f); // Warna pink (#ff99cc)
    public Color flashColor = Color.white; // Warna saat berkedip

    [Header("Juice Timers & Speeds")]
    public float chunkDelay = 0.4f;    // Waktu tunggu sebelum easeSlider menyusut
    public float drainSpeed = 7f;      // Kecepatan easeSlider menyusut
    public float flashDuration = 0.1f; // Lama kedipan putih

    [Header("Punch / Shake Settings")]
    public float punchScaleY = 1.3f;   // Seberapa besar bar memanjang ke atas saat hit
    public float springSpeed = 10f;    // Kecepatan kembali ke ukuran normal
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 3f;

    private float delayTimer = 0f;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Color currentColor; // Menyimpan warna darah saat ini sebelum di-flash

    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;

        if (easeFill != null)
        {
            easeFill.color = damageChunkColor;
        }
    }

    void Update()
    {
        
        if (transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, springSpeed * Time.deltaTime);
        }

      
        if (easeSlider.value != healthSlider.value)
        {
            delayTimer -= Time.deltaTime;

            // Hanya mulai menyusut jika waktu delay sudah habis
            if (delayTimer <= 0)
            {
                easeSlider.value = Mathf.Lerp(easeSlider.value, healthSlider.value, drainSpeed * Time.deltaTime);
            }
        }
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        
        easeSlider.maxValue = health;
        easeSlider.value = health;

        currentColor = healthGradient.Evaluate(1f);
        healthFill.color = currentColor;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        currentColor = healthGradient.Evaluate(healthSlider.normalizedValue);
        
        // Reset timer delay agar easeSlider berhenti sejenak setiap kali kena hit baru
        delayTimer = chunkDelay;

        // Panggil efek getaran, punch, dan flash
        StopAllCoroutines();
        StartCoroutine(HitEffects());
    }

    private IEnumerator HitEffects()
    {
        
        transform.localScale = new Vector3(originalScale.x, originalScale.y * punchScaleY, originalScale.z);

      
        healthFill.color = flashColor;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

           
            if (elapsed > flashDuration)
            {
                healthFill.color = currentColor;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

    
        transform.localPosition = originalPosition;
        healthFill.color = currentColor;
    }
}
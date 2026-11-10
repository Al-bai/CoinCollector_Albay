using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pengaturan Prefab Musuh")]
    public GameObject musuh1Prefab;
    public GameObject musuh2Prefab;
    public GameObject musuh3Prefab;
    public GameObject musuhBossPrefab;

    [Header("Pengaturan Titik Kemunculan")]
    public Transform[] titikSpawn; 

    [Header("Pengaturan Waktu & Gelombang (Wave)")]
    public float jedaWaktuSpawn = 4f; 
    public int jumlahBiasaSebelumBoss = 5; 

    private float timer;
    private int jumlahMusuhTerSpawn = 0;

    void Start()
    {
        
        timer = jedaWaktuSpawn;
    }

    void Update()
    {
        
        timer -= Time.deltaTime;

       
        if (timer <= 0f)
        {
            SpawnMusuh();
            
            // Reset timer
            timer = jedaWaktuSpawn;
        }
    }

    void SpawnMusuh()
    {
       
        if (titikSpawn.Length == 0)
        {
            Debug.LogWarning("Titik spawn belum diisi di Inspector!");
            return;
        }

        
        int indeksAcak = Random.Range(0, titikSpawn.Length);
        Transform titikTerpilih = titikSpawn[indeksAcak];

       
        jumlahMusuhTerSpawn++;

        
        if (jumlahMusuhTerSpawn % jumlahBiasaSebelumBoss == 0)
        {
            // Spawn Boss
            Instantiate(musuhBossPrefab, titikTerpilih.position, Quaternion.identity);
            Debug.Log("BOSS MUNCUL di posisi: " + titikTerpilih.position);
        }
        else
        {
            // Spawn Musuh Biasa
            Instantiate(musuh1Prefab, titikTerpilih.position, Quaternion.identity);
            Instantiate(musuh2Prefab, titikTerpilih.position, Quaternion.identity);
            Instantiate(musuh3Prefab, titikTerpilih.position, Quaternion.identity);
            Debug.Log("Musuh muncul.");
        }
    }
}

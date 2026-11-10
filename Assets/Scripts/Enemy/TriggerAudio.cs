using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioClip klipAudio;

    public void PutarAudioMati()
    {
        if (klipAudio != null)
        {
            Debug.Log("Fungsi Audio Mati Terpanggil!"); // Membantu melacak di Console

            // 1. Buat game object baru kosong
            GameObject objekAudio = new GameObject("TempAudioMati");
            
            // 2. Tambahkan komponen AudioSource
            AudioSource sumberAudio = objekAudio.AddComponent<AudioSource>();
            
            // 3. Masukkan klip dan atur menjadi suara 2D agar terdengar jelas
            sumberAudio.clip = klipAudio;
            sumberAudio.spatialBlend = 0f; // 0 = 2D (suara konstan), 1 = 3D (terpengaruh jarak)
            sumberAudio.volume = 1f; // Pastikan volume maksimal
            
            // 4. Putar suara
            sumberAudio.Play();
            
            // 5. Hancurkan objek audio ini setelah durasi suaranya selesai
            Destroy(objekAudio, klipAudio.length);
        }
        else
        {
            Debug.LogWarning("Klip Audio kosong! Belum dimasukkan ke Inspector.");
        }
    }
}

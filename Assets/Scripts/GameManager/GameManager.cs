using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour
{
        [SerializeField] private int skor = 0;

        public int totalKoin;
        private int koinTerkumpul = 0;
        public TMP_Text txt;

        void Start()
        {
        // TODO: hitung jumlah koin di scene saat mulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        UpdateScore();
        }

        public void AmbilKoin()
        {
            koinTerkumpul++;
            UpdateScore();

            // TODO: jika koinTerkumpul == totalKoin, panggil Menang()
            if ( koinTerkumpul == totalKoin ) Menang();
        
        }

        void Menang()
        {
           txt.text = "KAMU MENANG!!!";
        }

        void UpdateScore()
        {
           txt.text = "Score : " + koinTerkumpul;
        }

       
         void OnEnable()
         {
            Enemy.OnZombieMati += TambahSkorSaatZombieMati;
         }

         void OnDisable()
         {
            Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
         }

         void TambahSkorSaatZombieMati(Enemy zombieYangMati)
         {
            skor += 10;
            Debug.Log("Skor: " + skor);
         }

}

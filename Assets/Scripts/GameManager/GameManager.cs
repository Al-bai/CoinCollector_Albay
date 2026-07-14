using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour
{
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
            if ( koinTerkumpul >= totalKoin ) Menang();
        
        }

        void Menang()
        {
           txt.text = "KAMU MENANG!!!";
        }

        void UpdateScore()
        {
        txt.text = "Score : " + koinTerkumpul;
        }

}

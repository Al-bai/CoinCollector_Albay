using UnityEngine;

public class RecieverEvent : MonoBehaviour
{
    void OnEnable()
    {
        PemancarEvent.TekanTombol += TampilkanPesan;
    }

    void OnDisable()
    {
        PemancarEvent.TekanTombol -= TampilkanPesan;
    }

    void TampilkanPesan()
    {
       Debug.Log("Receiver menerima event TekanTombol");
    }
}

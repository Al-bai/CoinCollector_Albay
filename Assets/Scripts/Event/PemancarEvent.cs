using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PemancarEvent : MonoBehaviour
{
    public static event Action TekanTombol;

    // Update is called once per frame
    void Update()
    {
 

      if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
      {
        Debug.Log("Tombol Space ditekan");
        TekanTombol?.Invoke();
      }
    }
}

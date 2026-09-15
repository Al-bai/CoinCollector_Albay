using UnityEngine;

public class Peleton : Enemy
{
   
   public bool peleton = true;

   public override void Serang(){
    Debug.Log("Peleton menyerang");
   }
}

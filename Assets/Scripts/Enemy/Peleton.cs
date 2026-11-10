using UnityEngine;

public class Peleton : Enemy
{
   
   public bool peleton = true;

   public override void Serang(){
    Debug.Log("Peleton menyerang");
    base.Serang();
   }

   protected override void AturAnimasiDanArah()
   {
      base.AturAnimasiDanArah(); 
   }
}

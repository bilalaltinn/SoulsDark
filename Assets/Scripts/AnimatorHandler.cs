using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BA
{
    public class AnimatorHandler : MonoBehaviour
    {
        //Tanýmlamalar.
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            //StringToHash:Bu iþlev, bir dizeyi benzersiz bir tamsayýya dönüþtürür.
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement) // Animator bileþenindeki deðerleri güncellemek veya ayarlamak için kullanýlýr.
                                                                                           // Bu, karakter animasyonlarýný, geçiþleri veya parametreleri kontrol etmek
                                                                                           // için kullanýlan bir yardýmcý fonksiyon olabilir.
        {

            #region vertical

            //Burda dikey(vertical) deðiþkenin durumuna baðlý olarak baþta 0 olarak baþlattýðýmýz "v" deðerini arttýrýr ya da azaltýr.
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement <0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }


            #endregion

            #region Horizontal 
            //Burda yatay(horizontal) deðiþkenin durumuna baðlý olarak baþta 0 olarak baþlattýðýmýz "h" deðerini arttýrýr ya da azaltýr.
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if(horizontalMovement > 0.55f)
            {
                h = 1;  
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if(horizontalMovement < -0.55f)
            {
                h= -1;
            }
            else
            {
                h = 0;
            }

            #endregion


            //1."SetFloat" iþlevi, Animator bileþenindeki bir float tipi parametreyi ayarlamak için kullanýlýr
            //2.Ýlk parametre olan "vertical", ayarlanacak parametrenin adýný belirtir.
            //3.Ýkinci parametre olan "v", ayarlanacak deðeri belirtir. Bu deðer yukarýdaki kontrol yapýsýnda belirlenen "v" deðiþkenidir.
            //4.Üçüncü parametre olan "0.1f", geçiþ süresini belirtir.
            //5.Dördüncü parametre olan "Time.deltaTime", zaman aralýðýný belirtir.
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }


        public void OnParticleSystemStopped() //Bu fonksiyon, bir Particle System bileþeni durduðunda otomatik olarak çaðrýlacaktýr.
        {
            canRotate = false;
        }
    }
}

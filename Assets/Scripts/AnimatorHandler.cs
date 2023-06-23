using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BA
{
    public class AnimatorHandler : MonoBehaviour
    {
        //Tan�mlamalar.
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            //StringToHash:Bu i�lev, bir dizeyi benzersiz bir tamsay�ya d�n��t�r�r.
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement) // Animator bile�enindeki de�erleri g�ncellemek veya ayarlamak i�in kullan�l�r.
                                                                                           // Bu, karakter animasyonlar�n�, ge�i�leri veya parametreleri kontrol etmek
                                                                                           // i�in kullan�lan bir yard�mc� fonksiyon olabilir.
        {

            #region vertical

            //Burda dikey(vertical) de�i�kenin durumuna ba�l� olarak ba�ta 0 olarak ba�latt���m�z "v" de�erini artt�r�r ya da azalt�r.
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
            //Burda yatay(horizontal) de�i�kenin durumuna ba�l� olarak ba�ta 0 olarak ba�latt���m�z "h" de�erini artt�r�r ya da azalt�r.
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


            //1."SetFloat" i�levi, Animator bile�enindeki bir float tipi parametreyi ayarlamak i�in kullan�l�r
            //2.�lk parametre olan "vertical", ayarlanacak parametrenin ad�n� belirtir.
            //3.�kinci parametre olan "v", ayarlanacak de�eri belirtir. Bu de�er yukar�daki kontrol yap�s�nda belirlenen "v" de�i�kenidir.
            //4.���nc� parametre olan "0.1f", ge�i� s�resini belirtir.
            //5.D�rd�nc� parametre olan "Time.deltaTime", zaman aral���n� belirtir.
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }


        public void OnParticleSystemStopped() //Bu fonksiyon, bir Particle System bile�eni durdu�unda otomatik olarak �a�r�lacakt�r.
        {
            canRotate = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BA
{

    public class PlayerLocomation : MonoBehaviour
    {
        //Tan�mlamalar.
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        
        [HideInInspector] //inspector panelinde gizlenicek olanlar.
        public Transform myTransform;
        [HideInInspector] //inspector panelinde gizlenicek olanlar.
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;

        void Start()
        {
            //Yakalamalar.
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize(); //Initialize:bir �eyin ba�lat�lmas�, ayarlanmas� veya ba�lang�� durumuna getirilmesi anlam�na gelir

        }

        public void Update()
        {
            float delta=Time.deltaTime;

            inputHandler.TickInput(delta);
            //MoveDirectiona kamerayla y�n�yle dikey yap�lan hareketi �arparak e�itliyoruz.
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize(); ////Normalize: i�lemi, bir vekt�r�n uzunlu�unu 1'e (birim uzunluk) ayarlar, ancak y�n�n� korur.
            moveDirection.y = 0;

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        #region movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            // kamera y�n�yle dikey yap�lan hareketi targetDir'e ekliyoruz.
            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize(); //Normalize: i�lemi, bir vekt�r�n uzunlu�unu 1'e (birim uzunluk) ayarlar, ancak y�n�n� korur.
            targetDir.y = 0;

            if (targetDir==Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            float rs = rotationSpeed;
            //Quaternion:rotasyonlar�n do�ru bir �ekilde birle�tirilmesini ve hesaplanmas�n� sa�lar,nesnelerin d�nd�r�lmesi ve rotasyon i�lemlerinin ger�ekle�tirilmesi i�in �nemli.
            Quaternion tr =Quaternion.LookRotation(targetDir);
            Quaternion targetRotation=Quaternion.Slerp(myTransform.rotation, tr, rs * delta); // iki Quaternion aras�nda s�rekli bir d�n�� yapmay� sa�layan bir i�levdir.
                                                                                              // Slerp, iki nokta aras�nda yuvarlak bir yol izleyerek p�r�zs�z bir d�n�� etkisi
                                                                                              // elde etmek i�in kullan�l�r.

            myTransform.rotation = targetRotation;
        }


        #endregion


    }
}

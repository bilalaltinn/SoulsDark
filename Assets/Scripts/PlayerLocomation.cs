using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BA
{

    public class PlayerLocomation : MonoBehaviour
    {
        //Tanýmlamalar.
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
            animatorHandler.Initialize(); //Initialize:bir þeyin baþlatýlmasý, ayarlanmasý veya baþlangýç durumuna getirilmesi anlamýna gelir

        }

        public void Update()
        {
            float delta=Time.deltaTime;

            inputHandler.TickInput(delta);
            //MoveDirectiona kamerayla yönüyle dikey yapýlan hareketi çarparak eþitliyoruz.
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize(); ////Normalize: iþlemi, bir vektörün uzunluðunu 1'e (birim uzunluk) ayarlar, ancak yönünü korur.
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

            // kamera yönüyle dikey yapýlan hareketi targetDir'e ekliyoruz.
            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize(); //Normalize: iþlemi, bir vektörün uzunluðunu 1'e (birim uzunluk) ayarlar, ancak yönünü korur.
            targetDir.y = 0;

            if (targetDir==Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            float rs = rotationSpeed;
            //Quaternion:rotasyonlarýn doðru bir þekilde birleþtirilmesini ve hesaplanmasýný saðlar,nesnelerin döndürülmesi ve rotasyon iþlemlerinin gerçekleþtirilmesi için önemli.
            Quaternion tr =Quaternion.LookRotation(targetDir);
            Quaternion targetRotation=Quaternion.Slerp(myTransform.rotation, tr, rs * delta); // iki Quaternion arasýnda sürekli bir dönüþ yapmayý saðlayan bir iþlevdir.
                                                                                              // Slerp, iki nokta arasýnda yuvarlak bir yol izleyerek pürüzsüz bir dönüþ etkisi
                                                                                              // elde etmek için kullanýlýr.

            myTransform.rotation = targetRotation;
        }


        #endregion


    }
}

using UnityEngine;


namespace BA
{
    public class InputHandler : MonoBehaviour
    {
        //Tan�mlamalar.
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;
        CameraHandler cameraHandler;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);    
            }
        }

        public void OnEnable()
        {
            if (inputActions == null) //inputActionsun null olup olmama durumunu kontrol ediyor.
            {
                inputActions = new PlayerControls(); //Yeni bir atama.

                //performed: giri� eyleminin ger�ekle�ti�i anlam�na gelir ve tetiklenir (Oyuncunun bir tu�a basmas�n� bildiriyo.)
                //inputActions: Input System taraf�ndan sa�lanan giri� eylemlerini takip etmek ve i�lemek i�in kullan�lan bir nesne.
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();

                // i: Burda "i" ad�nda bir nesne olu�turduk ve o nesne ReadValue komutundan Vector2 yi �a��r�yor.
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable() //bir oyun nesnesinin devre d��� b�rak�ld��� durumda otomatik olarak �a�r�l�r.
        {
            inputActions.Disable(); //Yukarda inputActions'a atadad��m�z �zellikleri devre d��� b�rakt�.
        }

        public void TickInput(float delta) //bir oyunun her g�ncelleme ad�m�nda �a�r�lan ve giri� i�lemesini g�ncellemek i�in kullan�lan bir fonksiyonu temsil eder.
        {
            MoveInput(delta); //er g�ncelleme ad�m�nda �a�r�lan ve karakterin hareketini kontrol eden bir fonksiyonu �a��rmay� temsil eder.
                              //delta parametresi, giri� i�lemesini g�ncelleme aral���n� temsil eder..
        }

        public void MoveInput(float delta) 
        {
            horizontal = movementInput.x; 
            vertical = movementInput.y;

            //Clamp01:Unity'nin matematik fonksiyonlar�ndan biridir ve bir de�eri 0 ile 1 aras�nda s�n�rlar.
            //Abs:Unity'nin matematik fonksiyonlar�ndan biridir ve bir say�n�n mutlak de�erini d�nd�r�r.
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }


    }

}
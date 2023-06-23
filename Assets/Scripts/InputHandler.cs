using UnityEngine;


namespace BA
{
    public class InputHandler : MonoBehaviour
    {
        //Tanýmlamalar.
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

                //performed: giriþ eyleminin gerçekleþtiði anlamýna gelir ve tetiklenir (Oyuncunun bir tuþa basmasýný bildiriyo.)
                //inputActions: Input System tarafýndan saðlanan giriþ eylemlerini takip etmek ve iþlemek için kullanýlan bir nesne.
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();

                // i: Burda "i" adýnda bir nesne oluþturduk ve o nesne ReadValue komutundan Vector2 yi çaðýrýyor.
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable() //bir oyun nesnesinin devre dýþý býrakýldýðý durumda otomatik olarak çaðrýlýr.
        {
            inputActions.Disable(); //Yukarda inputActions'a atadadðýmýz özellikleri devre dýþý býraktý.
        }

        public void TickInput(float delta) //bir oyunun her güncelleme adýmýnda çaðrýlan ve giriþ iþlemesini güncellemek için kullanýlan bir fonksiyonu temsil eder.
        {
            MoveInput(delta); //er güncelleme adýmýnda çaðrýlan ve karakterin hareketini kontrol eden bir fonksiyonu çaðýrmayý temsil eder.
                              //delta parametresi, giriþ iþlemesini güncelleme aralýðýný temsil eder..
        }

        public void MoveInput(float delta) 
        {
            horizontal = movementInput.x; 
            vertical = movementInput.y;

            //Clamp01:Unity'nin matematik fonksiyonlarýndan biridir ve bir deðeri 0 ile 1 arasýnda sýnýrlar.
            //Abs:Unity'nin matematik fonksiyonlarýndan biridir ve bir sayýnýn mutlak deðerini döndürür.
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }


    }

}
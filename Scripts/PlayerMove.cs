using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    private bool _wheelZone = false;
    private bool _fishingAction = false;
    public bool _fishingZone = false;
    [SerializeField] private GameObject _fisher;
    [SerializeField] private GameObject _fisherBoat;
    
    public GameObject _parent;
    public GameObject _enabledCamera1;
    public GameObject _enabledCamera2;
    public GameObject _fishingCamera;
    public GameObject _fishingRod;
    public GameObject _fishingZoneButton;

    [SerializeField] private GameObject _gameManager;

    private float MouseX;
    private float MouseY;
    public float mouseSpeed;
    private Rigidbody rb;
   

    private void Start()
    {
        _fisherBoat.SetActive(true);
        _fishingRod.SetActive(false);
        rb = transform.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = 3;
        mouseSpeed = 20;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _wheelZone == true && _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled == true)
        {
            Debug.Log("Передаем управление плоту");
            _gameManager.GetComponent<GameManagerLogical>()._plotControllerEnabled = true;
            _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled = false;
            _parent = GameObject.Find("Plot");
            transform.parent = _parent.transform;
            _enabledCamera2.SetActive(true);
            _enabledCamera1.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.F) && _wheelZone == true && _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled == false)
        {
            Debug.Log("Передаем управление игроку");
            _gameManager.GetComponent<GameManagerLogical>()._plotControllerEnabled = false;
            _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled = true;
            _parent = GameObject.Find("PlayerBox");
            transform.parent = _parent.transform;
            _enabledCamera1.SetActive(true);
            _enabledCamera2.SetActive(false);
        }
        if (Input.GetKey(KeyCode.R) && _fishingAction == false && _fishingZone == true)
        {
            Debug.Log("Рыбалка активирована");
            _fishingAction = true;
            _fishingRod.SetActive(true);
            _fishingCamera.SetActive(true);
            _enabledCamera2.SetActive(false);
            _enabledCamera1.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.R) && _fishingAction == true && _fishingZone == true)
        {
            Debug.Log("Рыбалка дизактивирована");
            _fishingAction = false;
            _fishingRod.SetActive(false);
            _enabledCamera1.SetActive(true);
            _enabledCamera2.SetActive(false);
            _fishingCamera.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.E) && _fishingZone == true && _fishingAction == true)
        {
            Debug.Log("Рыбалка");
            _fishingAction = true;
            _fishingRod.SetActive(true);
            /// Какие то действия или анимация рыбалки
            /// 

            _fishingCamera.SetActive(true);
            _enabledCamera2.SetActive(false);
            _enabledCamera1.SetActive(false);

        }



        // Управление мышкой 
        MouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
       // MouseY = -Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, MouseX, 0); //MouseY
    }
    void OnTriggerEnter(Collider other)
        {
            Debug.Log("В зоне штурвала");
            if (_wheelZone == false)
            {
                _wheelZone = true;
            }

            if (other.tag == "FishingZone")
            {
                _fishingZoneButton.SetActive(true);
                _fishingZone = true;

            }


        }
        void OnTriggerExit(Collider other)
        {
            if (_wheelZone == true)
            {
                _wheelZone = false;
            }
            _fishingZoneButton.SetActive(false);
            _fishingZone = false;
    }




        void FixedUpdate()
        {
            if (_gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled == true && _gameManager.GetComponent<GameManagerLogical>()._plotControllerEnabled == false)
            {
                velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                transform.Translate(velocity.x, 0, velocity.y);
                StartCoroutine(Walk());
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                _fisherBoat.SetActive(false);
                SceneManager.LoadScene(1);
            }
        }

        IEnumerator Walk()
        {
            if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
                _fisher.GetComponent<Animator>().SetBool("Walking", true);
            else 
            {
                yield return new WaitForSeconds(0.1f);
                _fisher.GetComponent<Animator>().SetBool("Walking", false);
            }
        }
        
}

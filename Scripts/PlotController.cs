using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    [SerializeField] private GameObject _gameManager;
    private float MouseX;
    private float MouseY;
    public float mouseSpeed;
    private Rigidbody rb;
    private void Start()
    {
       
        rb = transform.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        speed = 3;
        mouseSpeed = 20;
    }
    void Update()
    {
        if (_gameManager.GetComponent<GameManagerLogical>()._plotControllerEnabled == true && _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled == false)
        {
            MouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            // MouseY = -Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, MouseX, 0); //MouseY
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
        }
    }
    void FixedUpdate()
    {
       
        
    } 
}

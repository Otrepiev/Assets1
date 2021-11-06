using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotController : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    [SerializeField] private GameObject _gameManager;


    void FixedUpdate()
    {
        if (_gameManager.GetComponent<GameManagerLogical>()._plotControllerEnabled == true &&  _gameManager.GetComponent<GameManagerLogical>()._playerControllerEnabled == false)
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
       }
        
    } 
}

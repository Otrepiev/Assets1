using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogical : MonoBehaviour
{
    public bool _playerControllerEnabled;
    public bool _plotControllerEnabled;
    public float _stamina;
    public float _water;
    
    public Slider _staminaSlider;
    public Slider _waterSlider;

    // Start is called before the first frame update
    void Start()
    {
    _playerControllerEnabled = true;
    _plotControllerEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage(1, 1);
        _staminaSlider.value = _stamina;
        _waterSlider.value = _water;
    }

    public void TakeDamage(float _deltaStamina, float _deltaWater)
    {
        _stamina -= Time.deltaTime * _deltaStamina;
        _water -= Time.deltaTime * _deltaWater;
    }
}


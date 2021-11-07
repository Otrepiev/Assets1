using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    [SerializeField] private Sprite[] _LF;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _arrows;
    private int _DifficulEnemy = 5;
    [SerializeField] private AudioClip[] _audioClips;
    public int[] _enemyArrowNumber;
    public int[] _playerArrowNumber;

        int x = 0;
    public bool FightStart;
    public bool StartPattern;
    public bool Ready;
    public bool _Bad;
    public bool _Good;

    public Slider _hpPlayer;
    public Slider _hpEnemy;
    public int _playerHp = 100;
    public int _enemyHp = 100;
    

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _enemy = GameObject.FindWithTag("Enemy");
        
        StartCoroutine(PatternEnemy());
        _playerArrowNumber = new int[_DifficulEnemy];
    }
    
    
    void Update()
    {
        if (Ready)
        {
            PatternPlayer();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(0);
        }
        //
        // if (StartPattern && _Good)
        // {
        //     StartCoroutine(StartFight());
        // }
    }

    IEnumerator PatternEnemy()
    {
        yield return new WaitForSeconds(7);
        _enemyArrowNumber = new int[_DifficulEnemy];
        for (int i = 0; i < _DifficulEnemy; i++)
        {
            Debug.Log("Start");
            _image.sprite = _LF[Random.Range(0, _LF.Length)];
            _arrows.SetActive(true);
            var index = Array.IndexOf(_LF, _image.sprite);
            _enemyArrowNumber[i] = index;
            WhatClip();
            yield return new WaitForSeconds(1);
        }

        Ready = true;
    }

    void PatternPlayer()
    {
        if (x < _DifficulEnemy)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _playerArrowNumber[x] = 0;
                x++;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                _playerArrowNumber[x] = 1;
                x++;
                
            }
            
        }

        if (x == _DifficulEnemy)
        {
            Sravnit();
            FightStart = true;
        }
    }
    
    void WhatClip()
    {
        if (_image.sprite.name == "Left")
        {
            _enemy.GetComponent<AudioSource>().clip = _audioClips[0];
            
            _enemy.GetComponent<AudioSource>().Play();   
        }
        else
        {
            _enemy.GetComponent<AudioSource>().clip = _audioClips[1];
            _enemy.GetComponent<AudioSource>().Play();   
        }
    }

    void Sravnit()
    {
        int count = 0;
        for (int i = 0; i < _DifficulEnemy; i++)
        {
            if (_playerArrowNumber[i] == _enemyArrowNumber[i])
            {
                count++;
            }
        }

        if (count == _DifficulEnemy)
        {
            _Good = true;
            _Bad = false;
        }
        else if (count != _DifficulEnemy)
        {
            _Bad = true;
        }

       StartCoroutine(StartFight());
    }

    IEnumerator StartFight()
    {
        FightStart = true;
        if (_Good && FightStart)
        {

            _player.GetComponent<Animator>().SetTrigger("Left");
            _enemy.GetComponent<Animator>().SetTrigger("Right");
            
            yield return new WaitForSeconds(1);
            
            _player.GetComponent<Animator>().SetTrigger("Kick");
            yield return new WaitForSeconds(0.9f);
            _enemy.GetComponent<Animator>().SetTrigger("Damaged");

            FightStart = false;
            x = 0;
            StartPattern = false;
        }
    }


    //
    //
    // IEnumerator EnemyHitAnimation()
    // {
    //     if (Ready)
    //     {
    //         _enemy.GetComponent<Animator>().SetBool("Kick", true);
    //     }
    //     yield return new WaitForSeconds(1);
    //     _enemy.GetComponent<Animator>().SetBool("Kick", false);
    //     if (FightOver)
    //     {
    //         _enemy.GetComponent<Animator>().SetTrigger("Die");
    //     }
    // }
    //
    // IEnumerator HumanControl()
    // {
    //     if (Input.GetKeyUp(KeyCode.Mouse0))
    //     {
    //         _player.GetComponent<Animator>().SetTrigger("DefLeft");
    //         DefencePlayerLeft = true;
    //         yield return new WaitForSeconds(0.5f);
    //         DefencePlayerLeft = true; 
    //     }
    //     if (Input.GetKeyUp(KeyCode.Mouse1))
    //     {
    //         _player.GetComponent<Animator>().SetTrigger("DefRight");
    //         DefencePlayerLeft = true;
    //         yield return new WaitForSeconds(0.5f);
    //         DefencePlayerLeft = true; 
    //     }
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    [SerializeField] private Sprite[] _LF;
    [SerializeField] private Image[] _images;
    [SerializeField] private GameObject[] _arrows;
    [SerializeField] private float _lvlEnemy;
    [SerializeField] private AudioClip[] _audioClips;

    public bool FightOver;
    public bool StartPattern;
    public bool Ready;
    public bool DefencePlayerLeft;
    public bool DefencePlayerRight;
    
        void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _enemy = GameObject.FindWithTag("Enemy");
    }
    
    
    void Update()
    {
        
        StartCoroutine(HumanControl());
    }

    IEnumerator Pattern() // Паттерн атаки. Должен появляться от "уровня" противника.
    {
        yield return new WaitForSeconds(3);
        _images[0].sprite = _LF[Random.Range(0, _LF.Length)];
        _arrows[0].SetActive(true);
        WhatClip(0);
        yield return new WaitForSeconds(1);
        _images[1].sprite = _LF[Random.Range(0, _LF.Length)];
        _arrows[1].SetActive(true);
        WhatClip(1);
        yield return new WaitForSeconds(1);
        _images[2].sprite = _LF[Random.Range(0, _LF.Length)];
        _arrows[2].SetActive(true); 
        WhatClip(2);
        yield return new WaitForSeconds(1);
        _images[3].sprite = _LF[Random.Range(0, _LF.Length)];
        _arrows[3].SetActive(true);
        WhatClip(3);
        yield return new WaitForSeconds(1);
    }

    void WhatClip(int d)
    {
        if (_images[d].sprite.name == "Left")
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



    IEnumerator EnemyHitAnimation()
    {
        if (Ready)
        {
            _enemy.GetComponent<Animator>().SetBool("Kick", true);
        }
        yield return new WaitForSeconds(1);
        _enemy.GetComponent<Animator>().SetBool("Kick", false);
        if (FightOver)
        {
            _enemy.GetComponent<Animator>().SetTrigger("Die");
        }
    }

    IEnumerator HumanControl()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _player.GetComponent<Animator>().SetTrigger("DefLeft");
            DefencePlayerLeft = true;
            yield return new WaitForSeconds(0.5f);
            DefencePlayerLeft = true; 
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _player.GetComponent<Animator>().SetTrigger("DefRight");
            DefencePlayerLeft = true;
            yield return new WaitForSeconds(0.5f);
            DefencePlayerLeft = true; 
        }
    }
}

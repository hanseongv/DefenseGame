using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEditor.Animations;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.5f;

    private Digimon _digimonInfo;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private List<Transform> _waypoints;
    private bool _isDie;

    [SerializeField]
    private int currentIndex = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Init()
    {
        _digimonInfo = null;
        _waypoints = null;
        transform.position = Vector3.zero;
        _spriteRenderer.sprite = null;
        currentIndex = 0;
        var color = _spriteRenderer.color;
        color.a = 1f;
        _spriteRenderer.color = color;
        _spriteRenderer.flipX = false;
        _isDie = false;
    }

    public void Spawn(List<Transform> wp, Digimon info)
    {
        Init();
        _digimonInfo = info;
        _waypoints = wp;
        transform.position = _waypoints[0].position;
        var controller = GameManager.Resources.GetResources<AnimatorController>($"Animator/Digimon/{_digimonInfo.id}");
        gameObject.SetActive(true);
        _animator.runtimeAnimatorController = controller;
        _animator.SetTrigger("doMove");
    }

    public void Die()
    {
        _isDie = true;

        _animator.SetTrigger("doDie");
        _spriteRenderer.DOFade(0f, 0.5f)
            .SetEase(Ease.InOutSine)
            // .SetLoops(3, LoopType.Yoyo)
            .OnComplete(() =>
            {
                GameManager.Event.OnDieEnemyHandle(this);
                gameObject.SetActive(false);
            });
    }

    public void Move()
    {
        if (_isDie || _waypoints == null || _waypoints.Count == 0) return;

        Transform target = _waypoints[currentIndex];
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );
        if (!((transform.position - target.position).sqrMagnitude < 0.0001f)) return;
        
        currentIndex = (currentIndex + 1) % _waypoints.Count;

        _spriteRenderer.flipX = currentIndex switch
        {
            0 or 3 => true,
            _ => false
        };
    }
}
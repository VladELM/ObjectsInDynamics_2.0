using static UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

[RequireComponent(typeof(BombExploder))]
public class Bomb : DestroyableObject
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private BombExploder _exploder;
    private float _maxAlpha = 1.0f;

    public event Action<Bomb> TimeCounted;

    private void Start()
    {
        _exploder = GetComponent<BombExploder>();
    }

    public override void Initialize(Vector3 position)
    {
        transform.position = position;
        Color color = new Color(_defaultMaterial.color.r, _defaultMaterial.color.g, _defaultMaterial.color.b, _maxAlpha);
        _defaultMaterial.color = color;

        StartCoroutine(Exploding(Range(_minLifeTime, _maxLifeTime)));
    }

    private IEnumerator Exploding(float lifeTime)
    {
        float currentTime = 0;
        bool isExploding = true;
        float delta = _defaultMaterial.color.a / (lifeTime / Time.deltaTime);

        while (isExploding)
        {
            yield return null;

            if (currentTime < lifeTime)
            {
                currentTime += Time.deltaTime;
                float alphaValue = Mathf.Lerp(_defaultMaterial.color.a, 0, delta);
                Color color = new Color(_defaultMaterial.color.r, _defaultMaterial.color.g, _defaultMaterial.color.b, alphaValue);
                _defaultMaterial.color = color;
            }
            else
            {
                isExploding = false;
                _exploder.Explode();
                TimeCounted?.Invoke(this);
            }
        }
    }
}

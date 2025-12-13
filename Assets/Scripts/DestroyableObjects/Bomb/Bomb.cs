using static UnityEngine.Random;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BombExploder))]
public class Bomb : DestroyableObject
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private BombExploder _exploder;
    private float _maxAlpha = 1.0f;

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
        float delta = _defaultMaterial.color.a / (lifeTime / Time.deltaTime);

        while (currentTime < lifeTime)
        {
            yield return null;

            currentTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(_defaultMaterial.color.a, 0, delta);
            Color color = new Color(_defaultMaterial.color.r, _defaultMaterial.color.g, _defaultMaterial.color.b, alphaValue);
            _defaultMaterial.color = color;
        }

        _exploder.Explode();
        CallEvent();
    }
}

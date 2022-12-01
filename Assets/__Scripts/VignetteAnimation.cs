using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteAnimation : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _maxIntensity;

    private Vignette _vignette;
    private float _minIntensity;

    private void Awake()
    {
        if (FindObjectOfType<Volume>().profile.TryGet(out Vignette vignette))
            _vignette = vignette;
        else
            Debug.LogWarning("Vignette not found");

        _minIntensity = _vignette.intensity.value;
    }

    private void OnEnable()
    {
        PlayerStats.OnHit += OnHit;
    }

    private void OnDisable()
    {
        PlayerStats.OnHit -= OnHit;
    }

    private IEnumerator PlayAnimation(float value, Color color)
    {
        var startIntensity = Mathf.Lerp(_minIntensity, _maxIntensity, value);
        var startValue = value;
        var startColor = color;

        float time = 0;
        for (int i = 1; i < _animationCurve.keys.Length; i++)
        {
            var key = _animationCurve.keys[i];
            var prevKey = _animationCurve.keys[i - 1];
            float t = time - (key.time - prevKey.time);
            float tMult = 1 / (key.time - prevKey.time);
            while (t < 1)
            {
                if (i > 1 && key.value < prevKey.value)
                    value = Mathf.Lerp(prevKey.value, key.value, t);
                else
                    value = Mathf.Lerp(0, key.value, t);

                value = Mathf.Clamp(value, 0, 1);
                var intensity = Mathf.Lerp(_minIntensity, _maxIntensity, value);
                color = _gradient.Evaluate(value);

                _vignette.intensity.value = intensity;
                _vignette.color.value = color;

                time += Time.deltaTime;
                t += Time.deltaTime * tMult;

                yield return null;
            }
        }
        _vignette.intensity.value = startIntensity;
        _vignette.color.value = startColor;

        yield return null;
    }

    private void OnHit(PlayerStats player)
    {
        float value = Mathf.InverseLerp(player.MaxHealth, 0, player.Health);
        var color = _gradient.Evaluate(value);

        StartCoroutine(PlayAnimation(value, color));
    }
}
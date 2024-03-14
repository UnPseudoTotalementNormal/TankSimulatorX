using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightExplosionScript : MonoBehaviour
{
    private HardLight2D _light2D;

    [SerializeField] private float _endAppearIntensity;
    [SerializeField] private float _endAppearRange;
    [SerializeField] private float _endAppearTime;
    [SerializeField] private AnimationCurve _endAppearCurve;

    [SerializeField] private float _endDisappearTime;
    [SerializeField] private AnimationCurve _endDisappearCurve;

    [SerializeField] private float _endDestroyTime;

    private void Awake()
    {
        _light2D = GetComponent<HardLight2D>();
        _light2D.Range = 0;
        _light2D.Intensity = 0;
        StartCoroutine(LightAnimation());
    }

    private IEnumerator LightAnimation()
    {
        float startAppearTime = Time.time;
        float endAppearTime = Time.time + _endAppearTime;
        while (Time.time < endAppearTime)
        {
            float curveValue = _endAppearCurve.Evaluate((Time.time - startAppearTime) / (endAppearTime - startAppearTime));
            _light2D.Range = _endAppearRange * curveValue;
            _light2D.Intensity = _endAppearIntensity * curveValue;
            yield return null;
        }
        _light2D.Range = _endAppearRange;
        _light2D.Intensity = _endAppearIntensity;

        float startDisappearTime = Time.time;
        float endDisappearTime = Time.time + _endDisappearTime;
        while (Time.time < endDisappearTime) 
        {
            float endCurveValue = _endDisappearCurve.Evaluate((Time.time - startDisappearTime) / (endDisappearTime - startDisappearTime));
            endCurveValue = Mathf.Abs(endCurveValue-1);
            _light2D.Range = _endAppearRange * endCurveValue;
            _light2D.Intensity = _endAppearIntensity * endCurveValue;
            yield return null;
        }
        Destroy(gameObject, _endDestroyTime);
        yield return null;
    }
}

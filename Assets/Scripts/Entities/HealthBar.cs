using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform _healthBarTransform;
    public float showHealthBarTimer;
    public float transitionSpeed;
    private float _timer;
    private Vector3 _scale;

    // Start is called before the first frame update
    void Awake()
    {
        _healthBarTransform = transform.Find("HealthValue");
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        ResetTimer();
    }

    public void SetHealthBar(float hp, float maxhp)
    {
        SetHealthBar(hp / maxhp);
    }

    public void SetHealthBar(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        Vector3 hpScale = Vector3.one;
        hpScale.x = percentage;
        _healthBarTransform.localScale = hpScale;
    }

    public void Update()
    {
        _timer += GameManager.GameTime;
        Vector3 targetScale = _timer < showHealthBarTimer ? Vector3.one : Vector3.zero;
        _scale = Vector3.MoveTowards(_scale, targetScale, transitionSpeed * GameManager.GameTime);
        if (transform.localScale != targetScale)
        {
            transform.localScale = _scale;
        }

        if (_timer > showHealthBarTimer && transform.localScale == targetScale)
            Destroy(gameObject);
    }

    public void ResetTimer()
    {
        _timer = 0;
    }
}

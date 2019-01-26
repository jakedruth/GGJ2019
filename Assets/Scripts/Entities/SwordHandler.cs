using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandler : MonoBehaviour
{
    public float length;
    public AnimationCurve rotationCurve;
    public AnimationCurve lengthCurve;
    private Coroutine attackingCoroutine;

    public void StartAttack(float angleIN )
    {
        if (attackingCoroutine != null)
            StopCoroutine(attackingCoroutine);

        attackingCoroutine = StartCoroutine(attack(angleIN));
    }

    private IEnumerator attack(float angleIN )
    {
        float attackTime = rotationCurve.keys[rotationCurve.keys.Length - 1].time;
        float timer = 0;
        while (timer < attackTime)
        {
            timer += GameManager.GameTime;

            float percentage = timer / attackTime;
            float angle = rotationCurve.Evaluate(timer);
            transform.rotation = Quaternion.AngleAxis(angleIN + angle, Vector3.forward);
            Vector3 scale = new Vector3(lengthCurve.Evaluate(timer) * length, 1, 1);
            transform.localScale = scale;
            yield return null;
        }

        attackingCoroutine = null;
    }

    public bool IsAttacking()
    {
        return attackingCoroutine != null;
    }
}

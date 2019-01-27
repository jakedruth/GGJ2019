using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public float coolDownTimer = 0.1f;
    private bool isCoolingDown;
    public UnityEngine.Events.UnityEvent hitBySword;

    public void ActivateSwitch()
    {
        if (isCoolingDown)
            return;

        StartCoroutine(SendEvent());
    }

    private IEnumerator SendEvent()
    {
        isCoolingDown = true;

        hitBySword.Invoke();

        yield return new WaitForSeconds(coolDownTimer * GameManager.gameSpeedMultiplier);

        isCoolingDown = false;
    }
}

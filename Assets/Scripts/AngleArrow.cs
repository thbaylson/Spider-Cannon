using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleArrow : MonoBehaviour
{
    [SerializeField] private Transform arrowPivot;
    [SerializeField] private Transform arrowSprite;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private float changeSpeed = 10f;

    public void MoveArrowUp()
    {
        currentAngle = Mathf.Clamp(currentAngle + changeSpeed * Time.deltaTime, minAngle, maxAngle);
    }

    public void MoveArrowDown()
    {
        currentAngle = Mathf.Clamp(currentAngle - changeSpeed * Time.deltaTime, minAngle, maxAngle);
    }

    public void UpdateArrowSprite(float chargeAmount)
    {
        float scale = Mathf.InverseLerp(0f, 100f, chargeAmount);
        arrowSprite.localScale = new Vector3(0.5f + scale * 1.5f, 1f, 1f);

        arrowPivot.localEulerAngles = new Vector3(0, 0, currentAngle);
    }

    public float GetLaunchAngle()
    {
        return currentAngle;
    }

    public void HandleLaunch()
    {
        currentAngle = 0f;
        HideUI();
    }

    private void HideUI()
    {
        arrowPivot.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        arrowPivot.gameObject.SetActive(true);
    }
}

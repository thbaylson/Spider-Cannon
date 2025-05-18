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
    public bool lengthMatchesForce;

    private SpiderLauncher spiderLauncher;
    private ChargeBar charge;

    private void Awake()
    {
        spiderLauncher = GetComponent<SpiderLauncher>();
        spiderLauncher.OnLaunched += HideUI;
        charge = GetComponent<ChargeBar>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentAngle = Mathf.Clamp(currentAngle + changeSpeed * Time.deltaTime, minAngle, maxAngle);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentAngle = Mathf.Clamp(currentAngle - changeSpeed * Time.deltaTime, minAngle, maxAngle);
        }

        if (lengthMatchesForce)
        {
            float scale = Mathf.InverseLerp(0f, 100f, charge.CurrentCharge);
            arrowSprite.localScale = new Vector3(0.5f + scale * 1.5f, 1f, 1f);
        }

        arrowPivot.localEulerAngles = new Vector3(0, 0, currentAngle);
        spiderLauncher.launchAngle = currentAngle;
    }

    public void HideUI()
    {
        arrowPivot.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        currentAngle = 0f;
        arrowPivot.gameObject.SetActive(true);
    }
}

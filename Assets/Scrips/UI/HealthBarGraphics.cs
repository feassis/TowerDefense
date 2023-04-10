using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarGraphics : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float fillSpeed;

    private float fillTarget;

    private void Awake()
    {
        fillImage.fillAmount = 1;
        fillTarget = 1;
    }

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        fillTarget = currentHP / maxHP;
    }

    void Update()
    {
        if(fillImage.fillAmount == fillTarget)
        {
            return;
        }

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, fillTarget, fillSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools;

public class WaveDisplayGraphics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float waveTextDuration = 1f;

    private void Awake()
    {
        ServiceLocator.RegisterService<WaveDisplayGraphics>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<WaveDisplayGraphics>();
    }

    public void ShowNewWave(string text)
    {
        waveText.text = text;

        StartCoroutine(WaveTextCoroutine());
    }

    private IEnumerator WaveTextCoroutine()
    {
        waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveTextDuration);

        waveText.gameObject.SetActive(false);
    }
}

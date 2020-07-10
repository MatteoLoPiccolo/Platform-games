using System;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnCoinsChange += PlayCoinAudio;
    }

    private void PlayCoinAudio(int coins)
    {
        audioSource.Play(); ;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChange -= PlayCoinAudio;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WalkyTalkyScript : MonoBehaviour {

    public AudioMixer _audioMixer;
    public AudioClip[] _clips;
    private AudioSource _audioSource;
    float _timer = 0f;

    void Start()
    {

    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _audioSource = GetComponent<AudioSource>();
        AudioClip clip = _clips[Random.Range(0, _clips.Length)];
        _audioSource.clip = clip;
        _timer = clip.length;
        IncreaseSound();
        _audioSource.Play();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            DisableSound();
            Disable();
        }
    }

    // Update is called once per frame
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void DisableSound()
    {
        _audioMixer.SetFloat("backgroundVolume", 0f);
        _audioMixer.SetFloat("effectVolume", 0f);
    }

    public void IncreaseSound()
    {
        _audioMixer.SetFloat("backgroundVolume", -5f);
        _audioMixer.SetFloat("effectVolume", -5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkyTalkyScript : MonoBehaviour {

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
        _audioSource.Play();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Disable();
        }
    }

    // Update is called once per frame
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

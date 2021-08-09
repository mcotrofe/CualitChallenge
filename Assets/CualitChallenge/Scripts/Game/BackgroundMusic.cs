using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] float volume = .5f;
    [SerializeField] float fadeTime = 1f;

    [SerializeField] AudioClip menu;
    [SerializeField] AudioClip combat;

    private AudioSource menuAudioSource;
    private AudioSource combatAudioSource;

    private Coroutine currentFadeCoroutine;


    void Start()
    {
        menuAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        menuAudioSource.clip = menu;
        combatAudioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        combatAudioSource.clip = combat;
        combatAudioSource.volume = menuAudioSource.volume = volume;
        combatAudioSource.loop = menuAudioSource.loop = true;
        menuAudioSource.Play();
    }

    public void PlayMenuMusic()
    {
        if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
        currentFadeCoroutine = StartCoroutine(FadeMusic(combatAudioSource, menuAudioSource));
    }

    public void PlayCombatMusic()
    {
        if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
        currentFadeCoroutine = StartCoroutine(FadeMusic(menuAudioSource, combatAudioSource));
    }

    private IEnumerator FadeMusic(AudioSource current, AudioSource next)
    {
        float t = 0;
        next.volume = 0;
        next.Play();
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            current.volume = Mathf.Lerp(volume, 0, t / fadeTime);
            next.volume = Mathf.Lerp(0, volume, t / fadeTime);
            yield return null;
        }
        current.Stop();
    }

}

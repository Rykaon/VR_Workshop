using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Debug.Log("MONGROSBOOL");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !");
            return;
        }
        s.source.Play();
    }

    public void Play(string name, GameObject target, float radius)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !");
            return;
        }

        AudioSource.PlayClipAtPoint(s.clip, target.transform.position, radius);
    }

    public void PlayVariation(string name, float diffPitch, float diffVolume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !");
            return;
        }
        //var pitchDeBase = s.source.pitch;
        //var volumeDeBase = s.source.volume;
        s.source.pitch = s.pitch + UnityEngine.Random.Range(-diffPitch, diffPitch);
        s.source.volume = s.volume + UnityEngine.Random.Range(-diffVolume, diffVolume);

        //Debug.Log(s.source.pitch);

        s.source.Play();
        //s.source.pitch = pitchDeBase;
        //s.source.volume = volumeDeBase;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found !");
            return;
        }
        s.source.Stop();
    }

    public void FadeOutAndStop(string name, float duration)
    {
        StartCoroutine(FadeOutAndStopCoroutine(name, duration));
    }

    IEnumerator FadeOutAndStopCoroutine(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float elapsedTime = 0f;
        float volume = s.source.volume;
        float VolumeToSet = 0;

        AnimationCurve curve = new AnimationCurve();
        int samplePoints = 10;

        for (int i = 0; i <= samplePoints; i++)
        {
            float time = (float)i / samplePoints;
            float value = DOVirtual.EasedValue(0, 1, time, Ease.InSine);
            curve.AddKey(time, value);
        }

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            volume = Mathf.Lerp(volume, VolumeToSet, curve.Evaluate(time));

            s.source.volume = volume;

            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        s.source.Stop();
    }

    public void FadeInAndPlay(string name, float duration, float volumeToSet)
    {
        StartCoroutine(FadeInAndPlayCoroutine(name, duration, volumeToSet));
    }

    IEnumerator FadeInAndPlayCoroutine(string name, float duration, float volumeToSet)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float elapsedTime = 0f;
        float volume = 0;
        AnimationCurve curve = new AnimationCurve();
        int samplePoints = 10;

        for (int i = 0; i <= samplePoints; i++)
        {
            float time = (float)i / samplePoints;
            float value = DOVirtual.EasedValue(0, 1, time, Ease.InSine);
            curve.AddKey(time, value);
        }

        Debug.Log(name);

        s.source.volume = volume;
        s.source.Play();

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            volume = Mathf.Lerp(volume, volumeToSet, curve.Evaluate(time));

            s.source.volume = volume;

            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void PitchShiftDown(string name)
    {
        StartCoroutine(PitchShiftDownCoroutine(name));
    }

    private IEnumerator PitchShiftDownCoroutine(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float startPitch = s.source.pitch;

        while (s.source.pitch > 0)
        {
            s.source.pitch -= Time.deltaTime * 0.06f;
            yield return null;
        }

        s.source.Stop();
        s.source.pitch = startPitch;
    }
}
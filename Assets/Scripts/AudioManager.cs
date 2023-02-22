using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject effectsSourcePrefab;
    [SerializeField] private AudioSource musicSource;

    private ObjectPool<AudioSource> effectsSourcesPool;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    // Singleton instance.
    public static AudioManager Instance = null;
    
    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);

        effectsSourcesPool = new ObjectPool<AudioSource>(
            CreateEffectSource,
            (obj) => obj.gameObject.SetActive(true),
            (obj) =>
            {
                obj.Stop();
                obj.gameObject.SetActive(false);
            },
            (obj) => Destroy(obj.gameObject),
            false,
            5,
            10
        );
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        var source = GetSoundEffectSource();
        source.clip = clip;
        source.Play();
        StartCoroutine(ReturnSoundEffectSource(source, clip.length));
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        var source = GetSoundEffectSource();
        source.pitch = randomPitch;
        source.clip = clips[randomIndex];
        source.Play();
        StartCoroutine(ReturnSoundEffectSource(source, clips[randomIndex].length));
    }

    private AudioSource CreateEffectSource()
    {
        AudioSource source = Instantiate(effectsSourcePrefab, Vector3.zero, Quaternion.identity, this.transform)
            .GetComponent<AudioSource>();
        return source;
    }

    private AudioSource GetSoundEffectSource()
    {
        return effectsSourcesPool.Get();
    }

    private void ClearEffectsPool()
    {
        effectsSourcesPool.Clear();
    }

    private IEnumerator ReturnSoundEffectSource(AudioSource source, float time)
    {
        yield return new WaitForSeconds(time);
        effectsSourcesPool.Release(source);
    }
}

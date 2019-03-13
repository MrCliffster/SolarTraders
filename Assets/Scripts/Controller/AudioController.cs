using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // ---------------- Soundtrack variables
    [SerializeField]
    private List<AudioClip> Soundtrack;
    [SerializeField]
    private AudioSource MusicPlayer;

    [SerializeField] // Change for different starting track
    private int CurrentTrack = 0;

    // ---------------- Sound FX variables
    public List<AudioClip> SoundFX;
    [SerializeField]
    private AudioSource FXPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Soundtrack stuff
        if (MusicPlayer == null)
        {
            Debug.LogError("Couldn't find a music player!");
        }
        MusicPlayer.clip = Soundtrack[CurrentTrack];
        MusicPlayer.Play();
        StartCoroutine(WaitForTrackToEnd());

        // Sound FX stuff
        if (FXPlayer == null)
        {
            Debug.LogError("Couldn't find an FX player!");
        }
    }

    public void PlaySoundEffect(AudioClip clip, bool isLooping)
    {
        StopSoundEffect();
        FXPlayer.clip = clip;
        FXPlayer.loop = isLooping;
        FXPlayer.Play();
    }

    public void StopSoundEffect()
    {
        FXPlayer.Stop();
    }

    public void NextTrack()
    {
        if (CurrentTrack < Soundtrack.Count)
        {
            CurrentTrack++;   
        }
        else
        {
            CurrentTrack = 0;
        }
        MusicPlayer.clip = Soundtrack[CurrentTrack];
        MusicPlayer.Play();
        StartCoroutine(WaitForTrackToEnd());
    }

    IEnumerator WaitForTrackToEnd()
    {
        while (MusicPlayer.isPlaying)
        {

            yield return new WaitForSeconds(0.01f);

        }
        NextTrack();
    }
}

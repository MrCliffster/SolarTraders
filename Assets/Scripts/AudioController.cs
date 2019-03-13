using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> Soundtrack;
    private AudioSource MusicPlayer;
    [SerializeField] // Change for different starting track
    private int CurrentTrack = 0;

    // Start is called before the first frame update
    void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
        if (MusicPlayer == null)
        {
            Debug.LogError("Couldn't find a music player!");
        }
        MusicPlayer.clip = Soundtrack[CurrentTrack];
        MusicPlayer.Play();
        StartCoroutine(WaitForTrackToEnd());
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

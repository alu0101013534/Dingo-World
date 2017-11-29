using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class VideoController : MonoBehaviour {

    public VideoPlayer videoPlayer;
    public AudioSource audioPlayer;
    public int audioDelayMS = 500;
    public VideoClip videoClip;
    public AudioClip audioClip;

	private void Start ()
    {
        audioPlayer.clip = audioClip;
        videoPlayer.clip = videoClip;
        videoPlayer.Prepare();

        if (audioDelayMS >= 0)
        {
            videoPlayer.Play();
            StartCoroutine(PlayAudioDelay());
        }
        else
        {
            audioPlayer.Play();
            StartCoroutine(PlayVideoDelay());
        }
    }

    private IEnumerator PlayVideoDelay()
    {
        yield return new WaitForSeconds(-audioDelayMS * .001f);
        videoPlayer.Play();
    }

    private IEnumerator PlayAudioDelay()
    {
        yield return new WaitForSeconds(audioDelayMS * .001f);
        audioPlayer.Play();
    }

    public void Skip()
    {
        videoPlayer.Stop();
        audioPlayer.Stop();

		Application.LoadLevel(2);
    }
}

using CandyCoded;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioClips
    {

        Slurp

    }

    [SerializeField]
    private AudioPoolReference _audioPoolReference;

    private void Awake()
    {
        _audioPoolReference.Populate();
    }

    public void Play(AudioClips audioClip)
    {

        Debug.Log("Playing " + audioClip.ToString());

        _audioPoolReference.Play(audioClip.ToString());
    }

}

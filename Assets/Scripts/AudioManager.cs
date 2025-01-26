using CandyCoded;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public enum AudioClips
    {

        Slurp,

        BubblePop

    }

    [SerializeField]
    private AudioPoolReference _audioPoolReference;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        _audioPoolReference.Populate();

        var pool = GameObject.Find("AudioPool (Auto)");

        if (pool != null)
        {
            DontDestroyOnLoad(pool);
        }
    }

    public void Play(AudioClips audioClip)
    {
        _audioPoolReference.Play(audioClip.ToString());
    }

}

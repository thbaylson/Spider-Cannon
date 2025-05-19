using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioClip[] musicTracks;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = audioSource.GetComponent<AudioSource>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        PlayRandomTrack();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomTrack();
        }
    }

    void PlayRandomTrack()
    {
        if (musicTracks.Length == 0) return;

        int index = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[index];
        audioSource.Play();
    }
}
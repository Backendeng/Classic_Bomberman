using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private string _mainSound = "Main";
    [SerializeField]
    private AudioMixerGroup _audioMixerGroup;
    [SerializeField]
    private Sound[] _sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _sounds.ToList().ForEach(SetUpAudioTitle);
            Play(_mainSound);
        }
    }

    public void Play(string title)
    {
        Sound sound = _sounds.FirstOrDefault(s => s.title.Equals(title));

        if (sound == null)
        {
            Debug.LogWarning("Sound with title: " + title + " not found!");
        }
        else
        {
            AudioSource audioSource = SetUpAudio(sound);
            sound.source.Play();
            if (!sound.shouldLoop)
            {
                Destroy(audioSource, sound.clip.length);
            }
        }
    }

    private void SetUpAudioTitle(Sound sound) => sound.title = sound.clip.name;

    private AudioSource SetUpAudio(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();

        sound.source.outputAudioMixerGroup = _audioMixerGroup;
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.shouldLoop;

        return sound.source;
    }
}

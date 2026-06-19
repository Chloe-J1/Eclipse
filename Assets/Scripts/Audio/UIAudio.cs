using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] m_buttonClick;
    [SerializeField] private AudioClip[] m_buttonHover;
    [SerializeField] private AudioClip[] m_startGame;
    [SerializeField] private AudioClip[] m_sliderPreview;



    private AudioSource m_source;
    private float m_lastSliderPlay = -999f;
    private void Awake()
    {
        m_source = GetComponent<AudioSource>();
       // DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonClick() => Play(m_buttonClick);
    public void PlayButtonHover() => Play(m_buttonHover);
    public void PlayStartGame() => Play(m_startGame);
   

    private void Play(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;
        m_source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    private void PlayClip(AudioClip clips)
    {
        m_source.PlayOneShot(clips);
    }

    public void PlayMasterSliderPreview(float volume)
    {
        if (Time.unscaledTime - m_lastSliderPlay < 0.25f) return;
        m_lastSliderPlay = Time.unscaledTime;
        m_source.volume = volume;
        AudioClip clip = m_sliderPreview[0];
        PlayClip(clip);
    }
    public void PlayAmbienceSliderPreview(float volume)
    {
        if (Time.unscaledTime - m_lastSliderPlay < 0.25f) return;
        m_lastSliderPlay = Time.unscaledTime;
        m_source.volume = volume;
        AudioClip clip = m_sliderPreview[1];
        PlayClip(clip);
    }

    public void PlayEnvSFXSliderPreview(float volume)
    {
        if (Time.unscaledTime - m_lastSliderPlay < 0.25f) return;
        m_lastSliderPlay = Time.unscaledTime;
        m_source.volume = volume;
        AudioClip clip = m_sliderPreview[2];
        PlayClip(clip);
    }
    public void PlayPlayerSliderPreview(float volume)
    {
        if (Time.unscaledTime - m_lastSliderPlay < 0.25f) return;
        m_lastSliderPlay = Time.unscaledTime;
        m_source.volume = volume;
        AudioClip clip = m_sliderPreview[3];
        PlayClip(clip);
    }

    public void PlayMusicSliderPreview(float volume)
    {
        if (Time.unscaledTime - m_lastSliderPlay < 0.25f) return;
        m_lastSliderPlay = Time.unscaledTime;
        m_source.volume = volume;
        AudioClip clip = m_sliderPreview[4];
        PlayClip(clip);
    }
}

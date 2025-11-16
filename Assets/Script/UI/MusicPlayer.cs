using UnityEngine;
using UnityEngine.SceneManagement;

// Hintergrund Musik wird durchgehen während des Spiels gespielt
public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private AudioSource audioSource;

    public static MusicPlayer Instance => instance;

    private void Awake()
    {
        // wenn schon ein MusicPlayer existiert → diesen zerstören
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // diesen als den globalen MusicPlayer markieren
        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlaying => audioSource.isPlaying;

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void SetMusicEnabled(bool enabled)
    {
        if (enabled)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
        else
        {
            // In allen anderen Szenen: Musik an (wenn nicht schon läuft)
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }
}

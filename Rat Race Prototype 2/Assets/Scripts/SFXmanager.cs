using UnityEngine;

public class SFXmanager : MonoBehaviour
{
    public static SFXmanager instance;

    [SerializeField] private AudioSource sfxObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip audioclip, Transform spawntransform, float volume)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawntransform.position, Quaternion.identity);

        audioSource.clip = audioclip;

        audioSource.volume = volume;

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}

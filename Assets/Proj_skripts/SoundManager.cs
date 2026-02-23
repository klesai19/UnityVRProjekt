using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip wormHitClip;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void PlayWormHit()
    {
        if (sfxSource == null || wormHitClip == null) return;
        sfxSource.PlayOneShot(wormHitClip);
    }
}
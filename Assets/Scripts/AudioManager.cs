using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] projectileInstantiateClips;
    [SerializeField] AudioClip[] projectileCancelClips;
    [SerializeField] AudioClip[] projectileHitClips;
    [SerializeField] AudioClip[] projectileSelfCancelClips;
    [SerializeField] AudioClip[] playerWinSFXs;
    [SerializeField] AudioClip[] roundStartSFXs;
    [SerializeField] AudioClip startSFX;
    [SerializeField] AudioSource BGMSource;


    float originalVolume;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RoundStart(int round)
    {
        originalVolume = BGMSource.volume;
        BGMSource.volume = 0.15f;
        PlaySoundEffect(roundStartSFXs[round - 1]);
        StartCoroutine(PlayStartSFX());
    }

    public void PlayerWin(int playerId)
    {
        PlaySoundEffect(playerWinSFXs[playerId]);
    }
    public void  PlayCancelSFX(int type)
    {
        PlaySoundEffect(projectileCancelClips[type]);
    }

    public void PlaySelfCancelSFX(int type)
    {
        PlaySoundEffect(projectileSelfCancelClips[type]);
    }
    public void PlayInstantiateSFX(int type)
    {
        PlaySoundEffect(projectileInstantiateClips[type]);
    }
    public void PlayHitSFX(int type)
    {
        PlaySoundEffect(projectileHitClips[type]);
    }

    public void PlaySoundEffect(AudioClip sfx)
    {
        AudioSource _as;
        _as = gameObject.AddComponent<AudioSource>();
        _as.clip = sfx;
        _as.Play();
        _as.loop = false;
        _as.volume = 3.0f;
        _as.priority = 0;
        StartCoroutine(DestroyAfterPlay(_as));
    }
    IEnumerator DestroyAfterPlay(AudioSource _as)
    {
        yield return new WaitForSeconds(_as.clip.length);
        Destroy(_as);
    }
    IEnumerator PlayStartSFX()
    {
        yield return new WaitForSeconds(4f);
        PlaySoundEffect(startSFX);
        yield return new WaitForSecondsRealtime(1.0f);
        BGMSource.volume = originalVolume;
    }
}

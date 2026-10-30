using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance{ get; set; }

    private AudioSource infantryAttackChannel;

    public AudioClip infantryAttackClip;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        infantryAttackChannel=gameObject.AddComponent<AudioSource>();
        infantryAttackChannel.volume=1.0f;
        infantryAttackChannel.playOnAwake=false;
    }

    public void PlayInfantryAttackSound()
    {
        if(!infantryAttackChannel.isPlaying)
        {
            infantryAttackChannel.PlayOneShot(infantryAttackClip);
        }
    }
}

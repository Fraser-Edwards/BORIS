using System.Collections;
using UnityEngine;

public class BoxSoundTrigger : MonoBehaviour,IPlayerRespawnListener
{
    public AudioParameters moogSoundClips;
    public bool isRespawnCheckPoint = true;

    private bool canTrigger = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() == null)
            return;

        if (moogSoundClips != null) SoundManager.PlayRandomSound(moogSoundClips.AudioClips, 1.0f, true);
        canTrigger = false;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() == null)
            return;

        canTrigger = true;
    }
       
    public void OnPlayerRespawnInThisCheckPoint(CheckPoint checkpoint, Player player)
    {
        if (isRespawnCheckPoint)
            gameObject.SetActive(true);
    }
}

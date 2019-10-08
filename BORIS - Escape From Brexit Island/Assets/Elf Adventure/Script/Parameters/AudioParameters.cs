using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="AudioParameters", menuName = "Audio Parameters")]
public class AudioParameters : ScriptableObject

{
    #region Variables

    public AudioClip[] AudioClips;

    static AudioParameters sm_instance;

    #endregion

    public static AudioParameters Instance
    {
        get
        {
            if (!sm_instance)
            {
                sm_instance = Resources.Load<AudioParameters>("Parameters/AudioParameters");
            }

            return sm_instance;
        }
    }
}

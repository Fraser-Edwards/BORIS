using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
/*
 * This is SoundManager
 * In other script, you just need to call SoundManager.PlaySfx(AudioClip) to play the sound
*/
public class SoundManager : MonoBehaviour {
	public static SoundManager Instance;

	[Tooltip("Play music clip when start")]
	public AudioClip musicsMenu;
	[Range(0,1)]
	public float musicMenuVolume = 0.5f;
	public AudioClip musicsGame;
	[Range(0,1)]
	public float musicsGameVolume = 0.5f;

	[Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
	public AudioClip soundClick;
	public AudioClip soundGamefinish;
	public AudioClip soundGameover;

    public AudioMixerGroup MusicBus;
    public AudioMixerGroup SFXBus;
    public AudioMixerGroup VocalBus;


    private AudioSource musicAudio;
	private AudioSource soundFx;
    private AudioSource voiceFx;

    //GET and SET
    public static float MusicVolume{
		set{ Instance.musicAudio.volume = value; }
		get{ return Instance.musicAudio.volume; }
	}
	public static float SoundVolume{
		set{ Instance.soundFx.volume = value; }
		get{ return Instance.soundFx.volume; }
	}
	// Use this for initialization
	void Awake(){
		Instance = this;
		musicAudio = gameObject.AddComponent<AudioSource> ();
		musicAudio.loop = true;
		musicAudio.volume = 0.5f;
		soundFx = gameObject.AddComponent<AudioSource>();
        voiceFx = gameObject.AddComponent<AudioSource>();

        musicAudio.outputAudioMixerGroup = MusicBus;
        soundFx.outputAudioMixerGroup = SFXBus;
        voiceFx.outputAudioMixerGroup = VocalBus;
    }
	void Start () {
//		//Check auido and sound
//		if (!GlobalValue.isMusic)
//			musicAudio.volume = 0;
//		if (!GlobalValue.isSound)
//			soundFx.volume = 0;
		PlayMusic(musicsGame,musicsGameVolume);
	}

	public static void PlaySfx(AudioClip clip, bool voice = false)
    {
		Instance.PlaySound(clip, voice ? Instance.voiceFx : Instance.soundFx);
	}

	public static void PlaySfx(AudioClip clip, float volume, bool voice = false)
    {
		Instance.PlaySound(clip, voice ? Instance.voiceFx : Instance.soundFx, volume);
	}
    public static void PlaySfxDelayed(AudioClip clip, float delay, float volume, bool voice = false)
    {
		Instance.PlaySound(clip, voice ? Instance.voiceFx : Instance.soundFx, delay, volume);
	}

	public static void PlayMusic(AudioClip clip){
		Instance.PlaySound (clip, Instance.musicAudio);
	}

	public static void PlayMusic(AudioClip clip, float volume){
		Instance.PlaySound (clip, Instance.musicAudio, volume);
	}

    public static void PlayRandomSound(AudioClip[] clips, float volume = 1.0f, bool voice = false)
    {
        Instance.PlayRandom(clips, voice ? Instance.voiceFx : Instance.soundFx, volume);
    }

    private void PlayRandom(AudioClip[] clips, AudioSource audioOut, float volume = 1.0f)
    {
        if (clips == null)
        {
            return;
        }

        int index = Random.Range(0, clips.Length);
        if (audioOut == voiceFx && !voiceFx.isPlaying)
        {
            audioOut.clip = clips[index];
            audioOut.Play();
        }
        else if (audioOut != voiceFx)
        {
            audioOut.PlayOneShot(clips[index]);
        }
    }

    private void PlaySound(AudioClip clip,AudioSource audioOut){
		if (clip == null) {
//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (audioOut == musicAudio) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume);
	}

	private void PlaySound(AudioClip clip,AudioSource audioOut, float volume){
		if (clip == null) {
//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

		if (audioOut == musicAudio) {
			audioOut.clip = clip;
			audioOut.Play ();
		} else
			audioOut.PlayOneShot (clip, SoundVolume * volume);
	}

    private void PlaySound(AudioClip clip,AudioSource audioOut, float delay, float volume){
		if (clip == null) {
//			Debug.Log ("There are no audio file to play", gameObject);
			return;
		}

        if (audioOut == musicAudio)
        {
            audioOut.clip = clip;
            audioOut.Play();
        }
        else
            audioOut.clip = clip;
            audioOut.volume = SoundVolume * volume;
            audioOut.PlayDelayed(delay);
	}

    
}

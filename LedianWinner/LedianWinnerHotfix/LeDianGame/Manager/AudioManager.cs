using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;
    private AudioSource BGAudioSource = null;
    private Dictionary<string, AudioClip> AudioDic;
    public float soundVolume=0.8f;
    public float bgVolume = 0.5f;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        BGAudioSource = transform.GetComponent<AudioSource>();
        BGAudioSource.volume = bgVolume;
        AudioDic = new Dictionary<string, AudioClip>();
        AudioClip [] audioClicps = Resources.LoadAll<AudioClip>("Music");
        for (int i = 0; i < audioClicps.Length; i++)
        {
            if (!AudioDic.ContainsKey(audioClicps[i].name))
            {
                AudioDic.Add(audioClicps[i].name, audioClicps[i]);
            }
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName">背景音乐名</param>
    /// <param name="volume">音量</param>
    public void PlayerBgAudio(string audioName,float volume = 0)
    {
        if (BGAudioSource != null)
        {
            if (AudioDic.ContainsKey(audioName))
            {
                if (AudioDic.ContainsKey(audioName))
                {


                    BGAudioSource.clip = AudioDic[audioName];
                }

            }
            if (volume != 0)
            {
                BGAudioSource.volume = volume;
                bgVolume = volume;
            }

            BGAudioSource.Play();
            BGAudioSource.pitch = 1f;//音高
            BGAudioSource.loop = true;
        }
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBgAudio()
    {
        if (BGAudioSource != null)
        {
            BGAudioSource.Pause();
        }
    }

    public void StopBgAudio()
    {
        if (BGAudioSource!=null)
        {
            if (BGAudioSource.clip != null)
            {
                BGAudioSource.Stop();
            }
        }
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetBgAudioVolume(float volume)
    {
        if (BGAudioSource != null)
        {
            BGAudioSource.volume = volume;
            bgVolume = volume;
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName">音效名</param>
    /// <param name="volume">音量</param>
    /// <param name="name">创建GameObject名字 默认为null</param>
    public void PlaySound(string audioName,string name= null)
    {
        if (audioName == null)
        {
            return;
        }
        else
        {
            GameObject go = new GameObject((name == null) ? "AudioClip" : name);
            AudioSource sound = go.AddComponent<AudioSource>();
            if (AudioDic.ContainsKey(audioName))
            {
                sound.clip = AudioDic[audioName];
                sound.pitch = 1f;
                sound.volume = soundVolume;
                sound.Play();
                StartCoroutine(SoundPlayEndDestroy(sound, go));
            }
            else
            {
                return;
            }
        }
    }
    /// <summary>
    /// 播放完音效删除物体
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    private IEnumerator SoundPlayEndDestroy(AudioSource audioSource,GameObject obj)
    {
        if (audioSource==null|| obj==null)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(audioSource.clip.length * Time.timeScale);
            Destroy(obj);
        }
    }
    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
    }
}

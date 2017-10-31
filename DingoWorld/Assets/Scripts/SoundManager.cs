/// <summary>
/// Sound manager.
/// This script use for manager all sound(bgm,sfx) in game
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	[System.Serializable]
	public class SoundGroup{
		public AudioClip audioClip;
		public string soundName;
	}

	public GameObject camera;
    private int lvl;
	private bool isFxEnabled;
	public  AudioSource bgmSound;
	
	public List<SoundGroup> sound_List = new List<SoundGroup>();
	
	public static SoundManager instance;
	
	public void Start(){
		instance = this;
        lvl = PlayerPrefs.GetInt("Level",1);
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		if(PlayerPrefs.GetInt("isMusicEnabled",1)==1)
		{
			Play();
		}
		else
		{
			Stop();
		}

		if(PlayerPrefs.GetInt("isSoundFxEnabled",1)==1)
		{
			isFxEnabled = true;
		}
		else
		{
			isFxEnabled=false;
		}
        //StartCoroutine(StartBGM());
    }

	public void Play(){


        lvl = PlayerPrefs.GetInt("Level",1);
      //  bgmSound.Stop();
        bgmSound.Play();

	}
	public void Stop(){

		bgmSound.Stop();
		
	}
    public void PlayUI()
    {
		bgmSound.Play();

    }
    public void StopUI()
    {


		bgmSound.Stop();

    }
    public void PlayingSound(string _soundName){
	
		if (isFxEnabled) {
			AudioSource.PlayClipAtPoint (sound_List [FindSound (_soundName)].audioClip, camera.transform.position);
		}
	}
	
	private int FindSound(string _soundName){
		int i = 0;
		while( i < sound_List.Count ){
			if(sound_List[i].soundName == _soundName){
				return i;	
			}
			i++;
		}
		return i;
	}
	
	void ManageBGM()
	{
		StartCoroutine(StartBGM());
	}
	
	//Start BGM when loading complete
	IEnumerator StartBGM()
	{
		yield return new WaitForSeconds(0.5f);
		
	/*	while(PatternSystem.instance.loadingComplete == false)
		{
			yield return 0;
		}*/
		
		Debug.Log("play");
		bgmSound.Play();
	}
	
}

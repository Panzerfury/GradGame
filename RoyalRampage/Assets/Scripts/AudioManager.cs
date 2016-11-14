﻿ using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	[Header ("-- Player --")]
	[SerializeField]
	private string dashPlayer;
	[SerializeField]
	private string spinPlay, 
		stompPlay;

	[Header ("-- Objects --")]
	[SerializeField]
	private string actionPlay;
	private string landing;
	private string hit;
	private string destruction;


	[Header ("-- Background --")]
	[SerializeField]
	private string ambiencePlay;
	[SerializeField]
	private string ambienceStop, 
			musicSystem;


	[Header ("-- Menu --")]
	[SerializeField]
	private string menuButton;
	[SerializeField]
	private string startButton;

	[Header ("-- Score Screen --")]
	[SerializeField]
	private string scoreScreenOpen;
	[SerializeField]
	private string starReward, 
        pointsCountingPlay, 
        pointsCountingStop;

	[Header ("-- In Game --")]
	[SerializeField]
	private string timerTick;
	[SerializeField]
	private string pointsRewarded, 
		objectiveAnnounced,
		objectiveCompleted;

	// extra
	private bool ambPlaying = false;
    public const float IN_MAIN_MENU = 1.0f;
    public const float IN_LEVEL = 2f;
    public const float IN_LEVEL_TIME_RUNNING_OUT = 3f;
    public const float IN_LEVEL_TIMES_UP = 4f;
    public const float IN_SCORE_SCREEN = 5f;
    public const float IN_INTRO_CUTSCENE = 6f;

    //************* Player *************

    void PlayerDash(){
		PlaySound (dashPlayer, GameManager.instance.player);
	}

	void PlayerSwirl(){
		PlaySound (spinPlay, GameManager.instance.player);
	}
	void PlayerStomp(){
		PlaySound (stompPlay, GameManager.instance.player);
	}

	//************** Objects **************

	void ObjectActionHit(GameObject obj){
		string objSwitch = obj.GetComponent<ObjectBehavior> ().soundSwitch;
		AkSoundEngine.SetSwitch ("Objects", objSwitch,obj);
		AkSoundEngine.SetSwitch ("Object_Actions", "Hit",obj);
		PlaySound (actionPlay, obj);
	}

	void ObjectActionDestruction(GameObject obj){
		string objSwitch = obj.GetComponent<ObjectBehavior> ().soundSwitch;
		AkSoundEngine.SetSwitch ("Objects", objSwitch,obj);
		AkSoundEngine.SetSwitch ("Object_Actions", "Destruction",obj);
		PlaySound (actionPlay, obj);
	}

	void ObjectActionLanding(GameObject obj){
		string objSwitch = obj.GetComponent<ObjectBehavior> ().soundSwitch;
		AkSoundEngine.SetSwitch ("Objects", objSwitch, obj);
		AkSoundEngine.SetSwitch ("Object_Actions", "Landing",obj);
		PlaySound (actionPlay, obj);
	}


	//**************Background **************
	void BackgroundAmbStart(){
		if (ambPlaying == false) {
			PlaySound (ambiencePlay, gameObject);
			ambPlaying = true;
		}
	}
	void BackgroundAmbStop(){
		
		//if (ambPlaying == true) {
			PlaySound (ambienceStop, gameObject);
			ambPlaying = false;
		//}
	}

	void BackgroundMusic(float state){
		print ("music start");
        switch ((int)state)
        {
            case (int)IN_MAIN_MENU:
            AkSoundEngine.SetState("Game_States", "In_Main_Menu");
            print("in main");
            break;
            case (int)IN_INTRO_CUTSCENE:
            AkSoundEngine.SetState("Game_States", "In_Intro_Cutscene");

            break;
            case (int)IN_LEVEL:
            AkSoundEngine.SetState("Game_States", "In_Level");
            print("in level");
            break;
            case (int)IN_LEVEL_TIME_RUNNING_OUT:
            AkSoundEngine.SetState("Game_States", "In_Level_Time_Running_Out");
            print("in level time running");
            break;
            case (int)IN_LEVEL_TIMES_UP:
            AkSoundEngine.SetState("Game_States", "In_Level_Times_Up");
            print("in level timesup");
            break;

            case (int)IN_SCORE_SCREEN:
            AkSoundEngine.SetState("Game_States", "In_Score_Screen");
            print("in score screen");
            break;


            }

        //AkSoundEngine.SetState("Game_States", "In_Level");
       // AkSoundEngine.PostEvent(musicSystem, );
        PlaySound(musicSystem, gameObject);
        // GameManager.instance.OnApplicationOpen -= BackgroundMusic;
    }

	//************** Menus **************
	void MenuButtons(){
        PlaySound(menuButton, gameObject);
       // AkSoundEngine.PostEvent(menuButton, gameObject);
    }

	void StartButton(){
        PlaySound(startButton, gameObject);
    }

	//************** Score screen **************
	void ScoreScreenOpen(){
        PlaySound(scoreScreenOpen, gameObject);
    }

	void CountingStars(){

	}

	void CountingPoints(){

	}

	//************** In Game **************
    void UpdateTickingTimer(float timeLeft)
    {
        AkSoundEngine.SetRTPCValue("Level_Time",timeLeft, gameObject);
        PlaySound(timerTick, gameObject);
    }

	void RewardingPoints(GameObject obj){
        PlaySound(pointsRewarded, gameObject);
        

    }

	void AnnouncingObjective(){

	}

	void CompletedObjective(){

	}

	//****** play sound ****************

	private void PlaySound(string eventName, GameObject audioSource){
		if(eventName == null || eventName == "")
			return;

		AkSoundEngine.PostEvent (eventName, audioSource);
		AkSoundEngine.RenderAudio ();
	}

	//Subscribing

	void OnEnable(){
		//player sound
		GameManager.instance.OnPlayerDash += PlayerDash;
		GameManager.instance.OnPlayerSwirl += PlayerSwirl;
		GameManager.instance.OnPlayerStomp += PlayerStomp;
       

        //object sounds
        GameManager.instance.OnObjectHit += ObjectActionHit;
		GameManager.instance.OnObjectDestructed += ObjectActionDestruction;
		GameManager.instance.OnObjectLanding += ObjectActionLanding;

		//Background sounds
		GameManager.instance.OnLevelLoad += BackgroundAmbStart;
		GameManager.instance.OnLevelUnLoad += BackgroundAmbStop;
		GameManager.instance.OnMusicStateChange += BackgroundMusic;

        //UI
        GameManager.instance.OnMenuButtonClicked += MenuButtons;
        GameManager.instance.OnStartButtonClicked += StartButton;
        GameManager.instance.OnScoreScreenOpen += ScoreScreenOpen;
        GameManager.instance.OnObjectDestructed += RewardingPoints;
        GameManager.instance.OnTimerUpdate += UpdateTickingTimer;


    }

	void OnDisable(){
		//BackgroundAmbStop ();
		//player sound
		GameManager.instance.OnPlayerDash -= PlayerDash;
		GameManager.instance.OnPlayerSwirl -= PlayerSwirl;
		GameManager.instance.OnPlayerStomp -= PlayerStomp;


		//object sounds
		GameManager.instance.OnObjectHit -= ObjectActionHit;
		GameManager.instance.OnObjectDestructed -= ObjectActionDestruction;
		GameManager.instance.OnObjectLanding -= ObjectActionLanding;

		//Background sounds
		GameManager.instance.OnLevelLoad -= BackgroundAmbStart;
		GameManager.instance.OnLevelUnLoad -= BackgroundAmbStop;
		GameManager.instance.OnMusicStateChange -= BackgroundMusic;

        //UI
        GameManager.instance.OnMenuButtonClicked -= MenuButtons;
        GameManager.instance.OnStartButtonClicked -= StartButton;
        GameManager.instance.OnScoreScreenOpen -= ScoreScreenOpen;
        GameManager.instance.OnObjectDestructed -= RewardingPoints;
        GameManager.instance.OnTimerUpdate -= UpdateTickingTimer;
    }
}

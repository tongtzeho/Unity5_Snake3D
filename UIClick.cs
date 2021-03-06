﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClick : MonoBehaviour {

	public GameObject mainui, selectui, pauseui, helpui, gameupui, gameclockui, endui, rankui, diffui;
	private UnityEngine.UI.Text endup, endscore, enddown, ranktext;
	private GameObject game;
	private Camera overlookcamera;
	private bool ongame;
	private float lockspace;
	private float holdonlose, holdonwin;
	public static int level, life, score, diff, dietime;
	public static bool gameover, win;

	void Start() {
		ongame = false;
		overlookcamera = GameObject.Find ("Camera/OverlookFixed").GetComponent<Camera> ();
		overlookcamera.depth = 1;
		lockspace = 0;
		life = 2;
		score = 0;
		dietime = 0;
		holdonlose = holdonwin = -1;
		gameover = false;
		win = false;
		mainui = GameObject.Find ("Canvas/Main");
		selectui = GameObject.Find ("Canvas/Select");
		pauseui = GameObject.Find ("Canvas/Pause");
		helpui = GameObject.Find ("Canvas/Help");
		gameupui = GameObject.Find ("Canvas/GameUp");
		gameclockui = GameObject.Find ("Canvas/GameClock");
		rankui = GameObject.Find ("Canvas/Rank");
		endui = GameObject.Find ("Canvas/End");
		diffui = GameObject.Find ("Canvas/Difficulty");
		endup = GameObject.Find ("Canvas/End/Up").GetComponent<UnityEngine.UI.Text> ();
		endscore = GameObject.Find ("Canvas/End/Score").GetComponent<UnityEngine.UI.Text> ();
		enddown = GameObject.Find ("Canvas/End/Down").GetComponent<UnityEngine.UI.Text> ();
		ranktext = GameObject.Find("Canvas/Rank/Text").GetComponent<UnityEngine.UI.Text> ();
		mainui.SetActive (true);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
	}

	public void onStartButtonClick() {
		mainui.SetActive (false);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (true);
	}

	public void StartGame() {
		mainui.SetActive (false);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
		lockspace = 0.3f;
		overlookcamera.depth = -1;
		Main.ispause = false;
		gameover = false;
		win = false;
		game = Instantiate (Resources.Load ("Game") as GameObject);
		gameupui.SetActive (true);
		gameclockui.SetActive (true);
		ongame = true;
	}

	public void selectlevel() {
		mainui.SetActive (false);
		selectui.SetActive (true);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
	}

	public void onEasyButtonClick() {
		diff = 1;
		level = 1;
		StartGame ();
		//selectlevel ();
	}

	public void onMediumButtonClick() {
		diff = 2;
		level = 1;
		StartGame ();
		//selectlevel ();
	}

	public void onHardButtonClick() {
		diff = 3;
		level = 1;
		StartGame ();
		//selectlevel ();
	}
	/*
	public void onLevel1ButtonClick() {
		level = 1;
		StartGame ();
	}

	public void onLevel2ButtonClick() {
		level = 2;
		StartGame ();
	}

	public void onLevel3ButtonClick() {
		level = 3;
		StartGame ();
	}*/

	public void onContinueButtonClick() {
		lockspace = 0.3f;
		mainui.SetActive (false);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		gameupui.SetActive (true);
		gameclockui.SetActive (true);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
		Main.ispause = false;
		Main.lockr = Main.lockleft = Main.lockright = true;
	}

	public void onBackButtonClick() {
		if (ongame) {
			Destroy (game);
			overlookcamera.depth = 1;
			ongame = false;
		}
		life = 2;
		score = 0;
		dietime = 0;
		mainui.SetActive (true);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		helpui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
	}

	public void onExitButtonClick() {
		if (ongame) {
			Destroy (game);
		}
		Application.Quit ();
	}

	public void onHelpButtonClick() {
		mainui.SetActive (false);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		helpui.SetActive (true);
		endui.SetActive (false);
		rankui.SetActive (false);
		diffui.SetActive (false);
	}

	public string[] readscore() {
		FileInfo t = new FileInfo(Application.persistentDataPath+"//Highsocre.txt");
		if (!t.Exists) {
			TextWriter tw = new StreamWriter(Application.persistentDataPath+"//Highsocre.txt");
			tw.WriteLine ("500 400 300 200 100");
			tw.Flush ();
			tw.Close ();
		}
		StreamReader sr = File.OpenText(Application.persistentDataPath+"//Highsocre.txt");
		string[] result = sr.ReadLine().Split(' ');
		sr.Close ();
		return result;
	}

	public void onRankButtonClick() {
		string[] line = readscore ();
		int i;
		ranktext.text = "";
		for (i = 0; i < 5; i++) {
			int temp = int.Parse (line [i]);
			ranktext.text += (i + 1).ToString () + ".            " + (temp / 10000000).ToString () + (temp / 1000000 % 10).ToString () + (temp / 100000 % 10).ToString () + (temp / 10000 % 10).ToString () + (temp / 1000 % 10).ToString () + (temp / 100 % 10).ToString () + (temp / 10 % 10).ToString () + (temp % 10).ToString () + "\n";
		}
		rankui.SetActive (true);
		mainui.SetActive (false);
		selectui.SetActive (false);
		pauseui.SetActive (false);
		gameupui.SetActive (false);
		gameclockui.SetActive (false);
		helpui.SetActive (false);
		endui.SetActive (false);
		diffui.SetActive (false);
	}

	private void updatescore(int score) {
		string[] line = readscore ();
		int[] nums = new int[6];
		int k = 0;
		bool usescore = false;
		int i;
		for (i = 0; i < 5; i++) {
			if (!usescore) {
				if (score > int.Parse (line [i])) {
					usescore = true;
					nums [k] = score;
					k++;
				}
				nums [k] = int.Parse (line [i]);
				k++;
			} else {
				nums [k] = int.Parse (line [i]);
				k++;
			}
		}
		if (k > 5) {
			TextWriter tw = new StreamWriter(Application.persistentDataPath+"//Highsocre.txt");
			tw.WriteLine (nums [0].ToString () + " " + nums [1].ToString () + " " + nums [2].ToString () + " " + nums [3].ToString () + " " + nums [4].ToString ());
			tw.Flush ();
			tw.Close ();
		}
	}

	void Update() {
		if (ongame) {
			if (gameover) {
				ongame = false;
				Destroy (game);
				life--;
				dietime++;
				if (life > 0) {
					StartGame ();
				} else {
					updatescore (score);
					overlookcamera.depth = 1;
					endup.text = "GAME OVER";
					endscore.text = (UIClick.score / 10000000).ToString () + (UIClick.score / 1000000 % 10).ToString () + (UIClick.score / 100000 % 10).ToString () + (UIClick.score / 10000 % 10).ToString () + (UIClick.score / 1000 % 10).ToString () + (UIClick.score / 100 % 10).ToString () + (UIClick.score / 10 % 10).ToString () + (UIClick.score % 10).ToString ();
					enddown.text = "准备返回主菜单...";
					endui.SetActive (true);
					mainui.SetActive (false);
					gameupui.SetActive (false);
					gameclockui.SetActive (false);
					selectui.SetActive (false);
					pauseui.SetActive (false);
					helpui.SetActive (false);
					rankui.SetActive (false);
					diffui.SetActive (false);
					holdonlose = 2f;
				}
			} else if (win) {
				ongame = false;
				int addscore = level * (1 + diff) * 5000 * (5 - Mathf.Min (4, dietime));
				score += addscore;
				Destroy (game);
				overlookcamera.depth = 1;
				dietime = 0;
				if (level < 3) {
					endup.text = "通过第" + level.ToString () + "关！\n得分 +" + addscore.ToString();
					enddown.text = "准备进入第" + (level + 1).ToString () + "关...";
				} else {
					endup.text = "恭喜你！全部通关！\n得分 +" + addscore.ToString();
					enddown.text = "准备返回主菜单...";
					updatescore (score);
				}
				endscore.text = (UIClick.score / 10000000).ToString () + (UIClick.score / 1000000 % 10).ToString () + (UIClick.score / 100000 % 10).ToString () + (UIClick.score / 10000 % 10).ToString () + (UIClick.score / 1000 % 10).ToString () + (UIClick.score / 100 % 10).ToString () + (UIClick.score / 10 % 10).ToString () + (UIClick.score % 10).ToString ();
				endui.SetActive (true);
				mainui.SetActive (false);
				gameupui.SetActive (false);
				gameclockui.SetActive (false);
				selectui.SetActive (false);
				pauseui.SetActive (false);
				helpui.SetActive (false);
				rankui.SetActive (false);
				diffui.SetActive (false);
				holdonwin = 2f;
			}
			lockspace = Mathf.Max (-0.001f, lockspace - Time.deltaTime);
			if (lockspace <= 0 && (Input.GetKey (KeyCode.Space) || ((Input.touchCount > 0 && Input.touches [Input.touchCount - 1].position.x < Screen.width / 10 && Input.touches [Input.touchCount - 1].position.y < Screen.height / 10)))) {
				lockspace = 0.3f;
				if (!Main.ispause) {
					Main.ispause = true;
					gameupui.SetActive (true);
					gameclockui.SetActive (false);
					mainui.SetActive (false);
					selectui.SetActive (false);
					pauseui.SetActive (true);
					helpui.SetActive (false);
					endui.SetActive (false);
					rankui.SetActive (false);
					diffui.SetActive (false);
				} else {
					gameupui.SetActive (true);
					gameclockui.SetActive (true);
					mainui.SetActive (false);
					selectui.SetActive (false);
					pauseui.SetActive (false);
					helpui.SetActive (false);
					Main.ispause = false;
					endui.SetActive (false);
					rankui.SetActive (false);
					diffui.SetActive (false);
				}
			}
		} else if (holdonlose > 0) {
			holdonlose -= Time.deltaTime;
			if (holdonlose < 0) {
				life = 2;
				overlookcamera.depth = 1;
				score = 0;
				dietime = 0;
				mainui.SetActive (true);
				gameupui.SetActive (false);
				gameclockui.SetActive (false);
				selectui.SetActive (false);
				pauseui.SetActive (false);
				helpui.SetActive (false);
				endui.SetActive (false);
				rankui.SetActive (false);
				diffui.SetActive (false);
			}
		} else if (holdonwin > 0) {
			holdonwin -= Time.deltaTime;
			if (holdonwin < 0) {
				if (level < 3) {
					level++;
					StartGame ();
				} else {
					life = 2;
					overlookcamera.depth = 1;
					score = 0;
					dietime = 0;
					mainui.SetActive (true);
					gameupui.SetActive (false);
					gameclockui.SetActive (false);
					selectui.SetActive (false);
					pauseui.SetActive (false);
					helpui.SetActive (false);
					endui.SetActive (false);
					rankui.SetActive (false);
					diffui.SetActive (false);
				}
			}
		}
	}
}

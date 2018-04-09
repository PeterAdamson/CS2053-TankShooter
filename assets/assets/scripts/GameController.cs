using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

		public Text CX1;
		public Text CX2;
		public Text CX3;
		public Text PX1;
		public Text PX2;
		public Text PX3;
		public Text endText;

		private int playerHasBeenHit;
		private int computerHasBeenHit;

		public GameObject playerTank;
		public GameObject computerTank;

	// Use this for initialization
	void Start () {
		CX1.text = "";
		CX2.text = "";
		CX3.text = "";
		PX1.text = "";
		PX2.text = "";
		PX3.text = "";
		endText.text = "";
		playerHasBeenHit = 0;
		computerHasBeenHit = 0;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void playerHit()
	{
		playerHasBeenHit += 1;
		if(playerHasBeenHit == 1)
		{
			PX1.text = "X";
		}
		if(playerHasBeenHit == 2)
		{
			PX2.text = "X";
		}
		if(playerHasBeenHit == 3)
		{
			PX3.text = "X";
			GameOverLose();
		}
	}

	public void computerHit()
	{
		computerHasBeenHit += 1;
		if(computerHasBeenHit == 1)
		{
			CX1.text = "X";
		}
		if(computerHasBeenHit == 2)
		{
			CX2.text = "X";
		}
		if(computerHasBeenHit == 3)
		{
			CX3.text = "X";
			GameOverWin();
		}
	}

	public void GameOverLose()
	{
		endText.text = "Game Over! You Lose!";
		playerTank.active = false;
		Time.timeScale = 0;
	}

	public void GameOverWin()
	{
		endText.text = "Game Over! You Win!";
		computerTank.active = false;
		Time.timeScale = 0;
	}

}

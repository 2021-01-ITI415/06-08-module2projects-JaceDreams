using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PyramidScoreboard : MonoBehaviour
{
	public static PyramidScoreboard S; // The singleton for Scoreboard

	[Header("Set in Inspector")]
	public GameObject prefabFloatingScore1;


	[SerializeField] private int _score = 0;
	[SerializeField] private string _scoreString;

	private Transform canvasTrans;

	// The score property also sets the scoreString
	public int score
	{
		get
		{
			return (_score);
		}
		set
		{
			_score = value;
			scoreString = _score.ToString("0");
		}
	}

	// The scoreString propery also sets the Text.text
	public string scoreString
	{
		get
		{
			return (_scoreString);
		}
		set
		{
			_scoreString = value;
			GetComponent<Text>().text = _scoreString;
		}
	}

	void Awake()
	{
		if (S == null)
		{
			S = this; // Set the private singleton
		}
		else
		{
			Debug.LogError("ERROR: Scoreboard.Awake(): S is already set!");
		}
		canvasTrans = transform.parent;
	}

	// When called by SendMessage, this adds the fs.score to this.score
	public void FSCallback(PyramidFloatingScore fs)
	{
		score += fs.score;
	}

	// This will Instantiate a new FloatingScore GameObject and initialize it.
	// It also returns a pointer to the FloatingSCore created so that the
	// calling function can do more with it (like set fontSizes, etc.)
	public PyramidFloatingScore CreateFloatingScore(int amt, List<Vector2> pts)
	{
		GameObject go = Instantiate<GameObject>(prefabFloatingScore1);
		go.transform.SetParent(canvasTrans);
		PyramidFloatingScore fs = go.GetComponent<PyramidFloatingScore>();
		fs.score = amt;
		fs.reportFinishTo = this.gameObject; // Set fs to call back to this
		fs.Init(pts);
		return (fs);
	}
}

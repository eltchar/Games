using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_variables_script : MonoBehaviour {

	public static int who_win;
	public static string player1_name = "Player1";
	public static string player2_name = "Player2";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public string get_player1_name()
	{
		return player1_name;
	}
	public void set_player1_name(string new_name)
	{
		player1_name=new_name;
	}
	public string get_player2_name()
	{
		return player2_name;
	}
	public void set_player2_name(string new_name)
	{
		player2_name=new_name;
	}

	public void set_who_win(int player_nr)
	{
		who_win=player_nr;
	}

	public int get_who_win()
	{
		return who_win;
	}
}

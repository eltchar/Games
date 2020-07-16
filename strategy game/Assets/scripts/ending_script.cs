using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ending_script : MonoBehaviour {

	game_variables_script game_variables_list;
	GameObject game_variables_container;
    private TextMeshProUGUI win_textbox;

	// Use this for initialization
	void Start ()
	{
		game_variables_container = GameObject.Find ("Game_variables");
		game_variables_list = game_variables_container.GetComponentInChildren<game_variables_script> ();
        win_textbox = GameObject.Find("Title_winner").GetComponent<TextMeshProUGUI>();

        if (game_variables_list.get_who_win()==0)
		{
			win_textbox.text = game_variables_list.get_player1_name() + " wins !";
		}
		else if (game_variables_list.get_who_win()==1)
		{
			win_textbox.text = game_variables_list.get_player2_name() + " wins !";
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void to_menu()
	{
		SceneManager.LoadScene (0);
	}
	public void exit_game()
	{
		Application.Quit();
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour {

	private GameObject button_new;
    private GameObject button_new_online;
    private GameObject button_back;
	private GameObject button_help;
	private GameObject button_exit;
	private GameObject button_option;
    private GameObject button_credits;
    private GameObject rules_view;
	private GameObject option_view;
    private GameObject credits_view;
    game_variables_script game_variables_list;
	GameObject game_variables_container;

	void Start() 
	{
		//linking objects to variables
		game_variables_container = GameObject.Find ("Game_variables");
		game_variables_list = game_variables_container.GetComponentInChildren<game_variables_script> ();
		button_new = GameObject.Find ("Button_new");
        button_new_online = GameObject.Find("Button_new_online");
        button_back = GameObject.Find ("Button_back");
		button_help = GameObject.Find ("Button_help");
		button_exit = GameObject.Find ("Button_exit");
        button_credits = GameObject.Find("Button_credits");
        button_option = GameObject.Find ("Button_option");
		rules_view = GameObject.Find ("Rules");
        option_view = GameObject.Find ("Options");
        credits_view = GameObject.Find("Credits");

        //hiding unused objects
        button_back.SetActive (false);
		rules_view.SetActive (false);
        option_view.SetActive (false);
        credits_view.SetActive(false);
    }

	public void quit_game()
	{
		Application.Quit();
	}
	public void show_instruction()
	{
		//hiding other buttons showing off the scroll menu
		button_new.SetActive (false);
        button_new_online.SetActive(false);
        button_help.SetActive (false);
		button_exit.SetActive (false);
		button_option.SetActive (false);
        button_credits.SetActive(false);
        button_back.SetActive (true);
		rules_view.SetActive (true);
    }
	public void show_options()
	{
		button_new.SetActive (false);
        button_new_online.SetActive(false);
        button_help.SetActive (false);
		button_exit.SetActive (false);
		button_option.SetActive (false);
        button_credits.SetActive(false);
        button_back.SetActive (true);
        option_view.SetActive (true);
    }
	public void back_to_menu()
	{
		button_new.SetActive (true);
        button_new_online.SetActive(true);
        button_help.SetActive (true);
		button_exit.SetActive (true);
		button_option.SetActive (true);
        button_credits.SetActive(true);
        button_back.SetActive (false);
		rules_view.SetActive (false);
        option_view.SetActive (false);
        credits_view.SetActive(false);
    }

    public void show_credits()
    {
        button_new.SetActive(false);
        button_new_online.SetActive(false);
        button_help.SetActive(false);
        button_exit.SetActive(false);
        button_option.SetActive(false);
        button_credits.SetActive(false);
        button_back.SetActive(true);
        credits_view.SetActive(true);
    }

    public void new_game()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1 );
	}
    public void new_game_online()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void set_player1_name(string value)
	{
		game_variables_list.set_player1_name (value);
	}

	public void set_player2_name(string value)
	{
		game_variables_list.set_player2_name (value);
	}

}

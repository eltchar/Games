using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class game_status : MonoBehaviour {

	public int current_player_moves=0;
	public int current_player_turns=0;
	public int current_player_id=0;
	bool turn_edned=true;
	public bool moved_already=false;
	public int current_selected_pawn=-1;
	private TextMeshProUGUI current_player_id_text;
    private TextMeshProUGUI current_player_moves_text;
    private TextMeshProUGUI current_player_turns_text;
	public Button main_button;
	public Button roll_button;
	game_variables_script game_variables_list;
	select_cube_script[] player1_pawn_script_list;
	select_cube_script[] player2_pawn_script_list;
	select_script_floor[] floor_script_list;
    GameObject game_variables_container;
	GameObject floor_container;
	GameObject player1_container;
	GameObject player2_container;
	Vector3 movement_calc;

	// Use this for initialization
	void Start ()
	{
		game_variables_container = GameObject.Find ("Game_variables");
		game_variables_list = game_variables_container.GetComponentInChildren<game_variables_script> ();
        current_player_id_text = GameObject.Find("current_player_value").GetComponent<TextMeshProUGUI>();
        current_player_moves_text = GameObject.Find("moves_value").GetComponent<TextMeshProUGUI>();
        current_player_turns_text = GameObject.Find("rotations_value").GetComponent<TextMeshProUGUI>();

        current_player_moves_text.text = "";
		current_player_turns_text.text = "";
		current_player_id_text.text = game_variables_list.get_player1_name ();
		floor_container = GameObject.Find ("board_full");
		floor_script_list = floor_container.GetComponentsInChildren<select_script_floor> ();
		player1_container = GameObject.Find ("Player1_pawns");
		player1_pawn_script_list = player1_container.GetComponentsInChildren<select_cube_script> ();
		player2_container = GameObject.Find ("Player2_pawns");
		player2_pawn_script_list = player2_container.GetComponentsInChildren<select_cube_script> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//debug codes
		if (Input.GetKey("q")) 
		{
			current_player_moves++;
			current_player_moves_text.text = current_player_moves.ToString ();
		}
		if (Input.GetKey("w")) 
		{
			if(current_player_moves>0)
			current_player_moves--;
			current_player_moves_text.text = current_player_moves.ToString ();
		}
		if (Input.GetKey("a")) 
		{
			current_player_turns++;
			current_player_turns_text.text = current_player_turns.ToString ();
		}
		if (Input.GetKey("s")) 
		{
			if(current_player_turns>0)
			current_player_turns--;
			current_player_turns_text.text = current_player_turns.ToString ();
		}
		if (current_player_moves > 0) 
		{
			roll_button.gameObject.SetActive (false);
		} 
		else 
		{
			roll_button.gameObject.SetActive (true);
		}
		//detecting if pawn is selected
		if (moved_already == false)
		{
			for (int i = 0; i < player1_pawn_script_list.Length; i++)
			{
				if (player1_pawn_script_list [i].clicked == true)
				{
					if (Is_move_not_oob(i,0)==true || current_player_turns > 0)
					{
						if (player1_pawn_script_list [i].is_selected == true)
						{
							for (int j = 0; j < player1_pawn_script_list.Length; j++)
							{
								player1_pawn_script_list [j].is_selected = false;
								player1_pawn_script_list [j].select_color ();
							}
							player1_pawn_script_list [i].is_selected = true;
							player1_pawn_script_list [i].select_color ();
							current_selected_pawn = i;
						} else
						{
							player1_pawn_script_list [i].select_color ();
							current_selected_pawn = -1;
						}
					}

					player1_pawn_script_list [i].clicked = false;
				}
			}
			for (int i = 0; i < player2_pawn_script_list.Length; i++)
			{
				if (player2_pawn_script_list [i].clicked == true)
				{
					if (Is_move_not_oob (i, 1) == true || current_player_turns > 0)
					{
						if (player2_pawn_script_list [i].is_selected == true)
						{
							for (int j = 0; j < player2_pawn_script_list.Length; j++)
							{
								player2_pawn_script_list [j].is_selected = false;
								player2_pawn_script_list [j].select_color ();
							}
							player2_pawn_script_list [i].is_selected = true;
							player2_pawn_script_list [i].select_color ();
							current_selected_pawn = i;
						} else
						{
							player2_pawn_script_list [i].select_color ();
							current_selected_pawn = -1;
						}
					}
					player2_pawn_script_list [i].clicked = false;
				}
			}
		}
		//detecting selected floor tile
		for (int i = 0; i < floor_script_list.Length; i++)
		{
			if (floor_script_list [i].clicked == true)
			{
				//checking which players turn is now
				if (current_player_id == 0)
				{
					//checking how pawn is rotated, x+ = up, x- = down, z+ = right, z-= left
					switch (player1_pawn_script_list [current_selected_pawn].current_rot)
					{
					//rotation up
					case 0:
						//check if move is possible
						if (Is_move_not_oob(current_selected_pawn,0)==true || current_player_turns > 0)
						{
							//checking for movement forward
							movement_calc = player1_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x > 0 && movement_calc.z == 0 && current_player_moves > 0 && movement_calc.x <= current_player_moves)
							{
                                    if(Is_move_not_coliding(current_selected_pawn,0, player1_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))))==true)
                                    {
                                        if(Is_pawn_colided_correctly(0, floor_script_list[i].pos)==true)
                                        {
                                            Move_forward(0, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
							}
						//checking for rotations
							else if (movement_calc.x == 0 && movement_calc.z == 1 && current_player_turns > 0)
							{
								Rotate_pawn (0, 3);
							} 
							else if (movement_calc.x == 0 && movement_calc.z == -1 && current_player_turns > 0)
							{
								Rotate_pawn (0, 1);
							}
						}
						break;
					//rotation right
					case 1:
						if (Is_move_not_oob(current_selected_pawn,0)==true || current_player_turns > 0)
						{
							movement_calc = player1_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x == 0 && movement_calc.z < 0 && current_player_moves > 0 && Mathf.Abs (movement_calc.z) <= current_player_moves)
							{
                                    if (Is_move_not_coliding(current_selected_pawn, 0, player1_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(0, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(0, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                                }
							else if (movement_calc.x == 1 && movement_calc.z == 0 && current_player_turns > 0)
							{
								Rotate_pawn (0, 0);
							}
							else if (movement_calc.x == -1 && movement_calc.z == 0 && current_player_turns > 0)
							{
								Rotate_pawn (0, 2);
							}
						}
						break;
					//rotation down
					case 2:
						if (Is_move_not_oob(current_selected_pawn,0)==true || current_player_turns > 0)
						{
						movement_calc = player1_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
						if (movement_calc.x<0 && movement_calc.z==0 && current_player_moves>0 && Mathf.Abs(movement_calc.x)<=current_player_moves)
						{
                                    if (Is_move_not_coliding(current_selected_pawn, 0, player1_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(0, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(0, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                                }
						else if (movement_calc.x==0 && movement_calc.z==1 && current_player_turns>0)
						{
							Rotate_pawn (0, 3);
						}
						else if (movement_calc.x==0 && movement_calc.z==-1 && current_player_turns>0)
						{
							Rotate_pawn (0, 1);
						}
						}
						break;
					case 3:
						//rotation left
						if (Is_move_not_oob(current_selected_pawn,0)==true || current_player_turns > 0)
						{
						movement_calc = player1_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
						if (movement_calc.x==0 && movement_calc.z>0 && current_player_moves>0 && movement_calc.z<=current_player_moves)
						{
                                    if (Is_move_not_coliding(current_selected_pawn, 0, player1_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(0, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(0, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                                }
						else if (movement_calc.x==1 && movement_calc.z==0 && current_player_turns>0)
						{
								Rotate_pawn (0, 0);
						}
						else if (movement_calc.x==-1 && movement_calc.z==0 && current_player_turns>0)
						{
								Rotate_pawn (0, 2);
						}
					}
						break;
					default:
						break;
					}
				}
				else if(current_player_id ==1)
				{
					//checking how pawn is rotated
					switch (player2_pawn_script_list [current_selected_pawn].current_rot)
					{
					//rotation up
					case 0:
						//check if move is possible
						if (Is_move_not_oob(current_selected_pawn,1)==true || current_player_turns > 0)
						{
							//checking for movement forward
							movement_calc = player2_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x > 0 && movement_calc.z == 0 && current_player_moves > 0 && movement_calc.x <= current_player_moves)
							{
                                    if (Is_move_not_coliding(current_selected_pawn, 1, player2_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(1, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(1, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                            }
							//checking for rotations
							else if (movement_calc.x == 0 && movement_calc.z == 1 && current_player_turns > 0)
							{
								Rotate_pawn (1, 3);
							} 
							else if (movement_calc.x == 0 && movement_calc.z == -1 && current_player_turns > 0)
							{
								Rotate_pawn (1, 1);
							}
						}
						break;
						//rotation right
					case 1:
						if (Is_move_not_oob(current_selected_pawn,1)==true || current_player_turns > 0)
						{
							movement_calc = player2_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x == 0 && movement_calc.z < 0 && current_player_moves > 0 && Mathf.Abs (movement_calc.z) <= current_player_moves)
							{
                                    if (Is_move_not_coliding(current_selected_pawn, 1, player2_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(1, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(1, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                            }
							else if (movement_calc.x == 1 && movement_calc.z == 0 && current_player_turns > 0)
							{
								Rotate_pawn (1, 0);
							}
							else if (movement_calc.x == -1 && movement_calc.z == 0 && current_player_turns > 0)
							{
								Rotate_pawn (1, 2);
							}
						}
						break;
						//rotation down
					case 2:
						if (Is_move_not_oob(current_selected_pawn,1)==true || current_player_turns > 0)
						{
							movement_calc = player2_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x<0 && movement_calc.z==0 && current_player_moves>0 && Mathf.Abs(movement_calc.x)<=current_player_moves)
							{
								if (Is_move_not_coliding(current_selected_pawn, 1, player2_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(1, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(1, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
							}
							else if (movement_calc.x==0 && movement_calc.z==1 && current_player_turns>0)
							{
								Rotate_pawn (1, 3);
							}
							else if (movement_calc.x==0 && movement_calc.z==-1 && current_player_turns>0)
							{
								Rotate_pawn (1, 1);
							}
						}
						break;
					case 3:
						//rotation left
						if (Is_move_not_oob(current_selected_pawn,1)==true || current_player_turns > 0)
						{
							movement_calc = player2_pawn_script_list [current_selected_pawn].current_pos - floor_script_list [i].pos;
							if (movement_calc.x==0 && movement_calc.z>0 && current_player_moves>0 && movement_calc.z<=current_player_moves)
							{
                                    if (Is_move_not_coliding(current_selected_pawn, 1, player2_pawn_script_list[current_selected_pawn].current_rot, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z)))) == true)
                                    {
                                        if (Is_pawn_colided_correctly(1, floor_script_list[i].pos) == true)
                                        {
                                            Move_forward(1, floor_script_list[i].pos, Mathf.RoundToInt((Mathf.Abs(movement_calc.x) + Mathf.Abs(movement_calc.z))));
                                        }
                                    }
                            }
							else if (movement_calc.x==1 && movement_calc.z==0 && current_player_turns>0)
							{
								Rotate_pawn (1, 0);
							}
							else if (movement_calc.x==-1 && movement_calc.z==0 && current_player_turns>0)
							{
								Rotate_pawn (1, 2);
							}
						}
						break;
					default:
						break;
					}
				}
				floor_script_list [i].clicked = false;
				moved_already = true;

			}
				
		}
	}

	public void Roll()
	{
		if (current_player_moves == 0 && turn_edned == true) {
			current_player_moves = Random.Range (1, 6);
			current_player_moves_text.text = current_player_moves.ToString ();
			current_player_turns = 1;
			current_player_turns_text.text = current_player_turns.ToString ();
			roll_button.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
			turn_edned = false;
		} 
		else if (current_player_moves == 0 && turn_edned == false) 
		{
			Is_it_game_over ();
			turn_edned = true;
			moved_already = false;
			current_selected_pawn = -1;
			if (current_player_id == 0)
			{
				current_player_id++;
				current_player_id_text.text = game_variables_list.get_player2_name ();
				current_player_turns = 0;
				current_player_moves_text.text = "";
				current_player_turns_text.text = "";
				for (int i = 0; i < player1_pawn_script_list.Length; i++)
				{
					player1_pawn_script_list [i].reset_color ();
				}

			}
			else 
			{
				current_player_id--;
				current_player_id_text.text = game_variables_list.get_player1_name ();
				current_player_turns = 0;
				current_player_moves_text.text = "";
				current_player_turns_text.text = "";
				for (int i = 0; i < player2_pawn_script_list.Length; i++)
				{
					player2_pawn_script_list [i].reset_color ();
				}
			}
			roll_button.GetComponentInChildren<TextMeshProUGUI>().text = "Roll";

		}
	}

	public void Menu_button_pressed()
	{
		SceneManager.LoadScene (0);
	}

	void Pawn_movement(Vector3 Pawn_pos, Vector3 Clicked_loc)
	{
		//Vector3 Travel_distance;
        //Travel_distance = Pawn_pos - Clicked_loc;
	}

	bool Is_move_not_oob(int pawn_nr, int player)
	{
		if (player==0)
			{
				switch (player1_pawn_script_list[pawn_nr].current_rot)
				{
				case 0:
				if (player1_pawn_script_list[pawn_nr].current_pos.x>0 || current_player_turns>0)
					{
					return true;
					}
					break;
				case 1:
				if (player1_pawn_script_list[pawn_nr].current_pos.z<15 || current_player_turns>0)
				{
					return true;
				}
					break;
				case 2:
				if (player1_pawn_script_list[pawn_nr].current_pos.x<6 || current_player_turns>0)
				{
					return true;
				}
					break;
				case 3:
				if (player1_pawn_script_list[pawn_nr].current_pos.z>0 || current_player_turns>0)
				{
					return true;
				}
					break;
				default:
					break;
				}
			}
		else if(player==1)
			{
			switch (player2_pawn_script_list[pawn_nr].current_rot) 
				{
			case 0:
				if (player2_pawn_script_list[pawn_nr].current_pos.x>0 || current_player_turns>0)
				{
					return true;
				}
				break;
			case 1:
				if (player2_pawn_script_list[pawn_nr].current_pos.z<15 || current_player_turns>0)
				{
					return true;
				}
				break;
			case 2:
				if (player2_pawn_script_list[pawn_nr].current_pos.x<6 || current_player_turns>0)
				{
					return true;
				}
				break;
			case 3:
				if (player2_pawn_script_list[pawn_nr].current_pos.z>0 || current_player_turns>0)
				{
					return true;
				}
				break;
				default:
				break;
				}
			}
			return false;
	}
    bool Is_move_not_coliding(int pawn_nr, int player, int rot, int distance)
    {
        Vector3 tempPos;
        if (player == 0)
        {
            //checking how pawn is rotated  x+ = up, x- = down, z+ = right, z-= left
            switch (player1_pawn_script_list[current_selected_pawn].current_rot)
            {
                //rotation up
                case 0:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player1_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.x = tempPos.x + i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                //rotation right
                case 1:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player1_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.z = tempPos.z + i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                //rotation down
                case 2:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player1_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.x = tempPos.x - i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                case 3:
                    //rotation left
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player1_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.z = tempPos.z - i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (player == 1)
        {
            switch (player2_pawn_script_list[current_selected_pawn].current_rot)
            {
                //rotation up
                case 0:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player2_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.x = tempPos.x + i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                //rotation right
                case 1:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player2_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.z = tempPos.z + i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                //rotation down
                case 2:
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player2_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.x = tempPos.x - i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                case 3:
                    //rotation left
                    for (int i = 1; i < distance; i++)
                    {
                        tempPos = player2_pawn_script_list[current_selected_pawn].current_pos;
                        tempPos.z = tempPos.z - i;
                        if (Is_space_occupied(tempPos) == true)
                            return false;
                    }
                    break;
                default:
                    break;
            }
        }
        return true;
    }
    bool Is_space_occupied(Vector3 pos)
    {
        for (int i = 0; i < player1_pawn_script_list.Length; i++)
        {
            if (pos == player1_pawn_script_list[i].current_pos)
                return true;
            if (pos == player2_pawn_script_list[i].current_pos)
                return true;
        }
        return false;
    }
    void Move_forward (int player, Vector3 end_pos, int distance)
	{
		if (player==0)
		{
			player1_pawn_script_list [current_selected_pawn].set_pos (end_pos);
			current_player_moves = current_player_moves - distance;
			current_player_moves_text.text = current_player_moves.ToString ();
			if (current_player_moves == 0 && current_player_turns == 0)
			{
				player1_pawn_script_list [current_selected_pawn].reset_color ();
				player1_pawn_script_list [current_selected_pawn].is_selected = false;
			}
			if (Is_move_not_oob(current_selected_pawn,0)==false && current_player_turns == 0)
			{
				current_player_moves = 0;
				current_player_moves_text.text = current_player_moves.ToString ();
				player1_pawn_script_list [current_selected_pawn].is_selected = false;
				player1_pawn_script_list [current_selected_pawn].select_color ();
			} 
			else
			{
				moved_already = true;
			}
		} 
		else if(player==1)
		{
			player2_pawn_script_list [current_selected_pawn].set_pos (end_pos);
			current_player_moves = current_player_moves - distance;
			current_player_moves_text.text = current_player_moves.ToString ();
			if (current_player_moves == 0 && current_player_turns == 0)
			{
				player2_pawn_script_list [current_selected_pawn].reset_color ();
				player2_pawn_script_list [current_selected_pawn].is_selected = false;
			}
			if (Is_move_not_oob(current_selected_pawn,1)==false && current_player_turns == 0)
			{
				current_player_moves = 0;
				current_player_moves_text.text = current_player_moves.ToString ();
				player2_pawn_script_list [current_selected_pawn].is_selected = false;
				player2_pawn_script_list [current_selected_pawn].select_color ();
			} 
			else
			{
				moved_already = true;
			}
		}

	}
	void Rotate_pawn(int player, int side)
	{
		if (player==0)
		{
			player1_pawn_script_list [current_selected_pawn].set_rot (side);
			current_player_turns = current_player_turns - 1;
			current_player_turns_text.text = current_player_turns.ToString ();
			if (current_player_moves == 0 && current_player_turns == 0)
			{
				player1_pawn_script_list [current_selected_pawn].reset_color ();
				player1_pawn_script_list [current_selected_pawn].is_selected = false;
			}
			if (Is_move_not_oob(current_selected_pawn,0)==false && current_player_turns == 0)
			{
				current_player_moves = 0;
				current_player_moves_text.text = current_player_moves.ToString ();
				player1_pawn_script_list [current_selected_pawn].is_selected = false;
				player1_pawn_script_list [current_selected_pawn].select_color ();
			} 
			else
			{
				moved_already = true;
			}
		} 
		else if(player==1)
		{
			player2_pawn_script_list [current_selected_pawn].set_rot (side);
			current_player_turns = current_player_turns - 1;
			current_player_turns_text.text = current_player_turns.ToString ();
			if (current_player_moves == 0 && current_player_turns == 0)
			{
				player2_pawn_script_list [current_selected_pawn].reset_color ();
				player2_pawn_script_list [current_selected_pawn].is_selected = false;
			}
			if (Is_move_not_oob(current_selected_pawn,1)==false && current_player_turns == 0)
			{
				current_player_moves = 0;
				current_player_moves_text.text = current_player_moves.ToString ();
				player2_pawn_script_list [current_selected_pawn].is_selected = false;
				player2_pawn_script_list [current_selected_pawn].select_color ();
			} 
			else
			{
				moved_already = true;
			}
		}

	}

	bool Is_pawn_colided_correctly(int player,Vector3 end_pos)
	{
		if (player==0)
		{
			for (int i = 0; i < player2_pawn_script_list.Length; i++)
			{
				if (end_pos == player2_pawn_script_list[i].current_pos && player2_pawn_script_list[i].alive==true)
				{
					player2_pawn_script_list [i].kill_pawn ();
				}
                if (end_pos == player1_pawn_script_list[i].current_pos)
                {
                    return false;
                }
            }
		} 
		else if(player==1)
		{
			for (int i = 0; i < player1_pawn_script_list.Length; i++)
			{
				if (end_pos == player1_pawn_script_list[i].current_pos && player1_pawn_script_list[i].alive==true)
				{
					player1_pawn_script_list [i].kill_pawn ();
				}
                if (end_pos == player2_pawn_script_list[i].current_pos)
                {
                    return false;
                }
            }
		}
        return true;
	}
	void Is_it_game_over()
	{
		int dead_bodies_counter = 0;
		for (int i = 0; i < player1_pawn_script_list.Length; i++)
		{
			if (player1_pawn_script_list [i].alive == false)
				dead_bodies_counter++;
		}
		if (dead_bodies_counter==player1_pawn_script_list.Length)
		{
			game_variables_list.set_who_win (1);
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1 );
		}
		dead_bodies_counter = 0;
		for (int i = 0; i < player2_pawn_script_list.Length; i++)
		{
			if (player2_pawn_script_list [i].alive == false)
				dead_bodies_counter++;
		}
		if (dead_bodies_counter==player1_pawn_script_list.Length)
		{
			game_variables_list.set_who_win (0);
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1 );
		}
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_cube_script : MonoBehaviour {

	game_status game_manager;
	public bool is_selected=false;
	public bool clicked=false;
	public int player_nr;
	public int current_rot;
	public bool alive = true;
	public Vector3 current_pos;
	Transform cube_transform;
	Renderer cube_renderer;
	Color color_of_cube;
	Color color_of_transparent;

	//rotations ^ - 0, > - 1, v - 2, < - 3, 

	// Use this for initialization
	void Start () 
	{
		game_manager = GameObject.FindObjectOfType<game_status>();
		cube_renderer=this.GetComponentInChildren<Renderer> ();
		color_of_cube = cube_renderer.material.color;
		color_of_transparent = color_of_cube;
		color_of_transparent.a = 0;
		cube_transform=this.GetComponentInChildren<Transform> ();
		current_pos= cube_transform.position;
		if (this.tag == "player1")
		{
			player_nr = 0;
			current_rot = 1;
		}
		if (this.tag == "player2")
		{
			player_nr = 1;
			current_rot = 3;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
		if (game_manager.current_player_id == player_nr)
		{
			if (game_manager.current_player_moves > 0) 
			{
				if (is_selected == true)
				{
					is_selected = false;
					clicked = true;
				} 
				else 
				{
					is_selected = true;
					clicked = true;
				}

			}
		} 
		else 
		{
			return;
		}

	}
	public void select_color()
	{
		if (is_selected == true) 
		{
			cube_renderer.material.color = Color.green;
		} 
		else 
		{
			cube_renderer.material.color = color_of_cube;
		}
	}
	public void reset_color()
	{
		cube_renderer.material.color = color_of_cube;
	}

	public void set_pos(Vector3 new_pos)
	{
		cube_transform.position = new_pos;
		current_pos = new_pos;
	}

	public void set_rot(int pos)
	{
		switch (pos)
		{
		case 0:
			if (current_rot == 3)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, 1);
				}
			} else if (current_rot == 1)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, -1);
				}
			}
			current_rot = 0;

			break;
		case 1:
			if (current_rot == 0)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, 1);
				}
			} else if (current_rot == 2)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, -1);
				}
			}
			current_rot = 1;
			break;
		case 2:
			if (current_rot == 1)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, 1);
				}
			} else if (current_rot == 3)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, -1);
				}
			}
			current_rot = 2;
			break;
		case 3:
			if (current_rot == 2)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, 1);
				}
			} else if (current_rot == 0)
			{
				for (int i = 0; i < 90; i++)
				{
					cube_transform.RotateAround (cube_transform.position, Vector3.up, -1);
				}
			}
			current_rot = 3;
			break;
		default:
			break;
		}
	}

	public void kill_pawn()
	{
		cube_transform.position = cube_transform.position + (Vector3.down * 5);
		cube_renderer.material.color = color_of_transparent;
		alive = false;
	}

}

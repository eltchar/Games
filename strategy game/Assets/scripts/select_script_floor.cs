using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_script_floor : MonoBehaviour {

	game_status game_manager;
	//public bool is_selected=false;
	public bool clicked=false;
	bool is_mouse_over=false;
	public Vector3 pos;
	Transform cube_transform;
	Renderer cube_renderer;
	Color cube_color_transparent;
	Color cube_color_visible;
	float increment=0.01f;

	// Use this for initialization
	void Start () 
	{
		game_manager = GameObject.FindObjectOfType<game_status>();
		cube_renderer=this.GetComponentsInChildren<Renderer>()[1];
		cube_transform=this.GetComponentsInChildren<Transform>()[0];
		pos = cube_transform.position;
		cube_color_transparent = cube_renderer.material.color;
		cube_color_visible = cube_renderer.material.color;
	}
	

	void OnMouseOver()
	{
		//if you mouse over on your turn highlight the field
		is_mouse_over=true;
		//cube_renderer.material.color = Color.Lerp (cube_color_transparent, cube_color_visible, Time.deltaTime);
	}

	void OnMouseExit()
	{
		//if you end mouse over on your turn off highlight from the field
		is_mouse_over=false;
		cube_renderer.material.color = cube_color_transparent;
	}
		
	void Update()
	{
		if (is_mouse_over == true)
		{
			if (cube_color_visible.a > 0.40f)
				increment = -0.01f;
			if (cube_color_visible.a < 0.0f)
				increment = 0.01f;
			cube_color_visible.a += increment;
			cube_renderer.material.color = cube_color_visible;
		}
	}

	void OnMouseUp()
	{
		if ((game_manager.current_player_moves > 0 || game_manager.current_player_turns >0) && game_manager.current_selected_pawn>-1) 
			{
				/*if (is_selected == true)
				{
					is_selected = false;
					clicked = true;
				} 
				else 
				{
					is_selected = true;
					clicked = true;
				}*/
				clicked = true;
				

			}

	}

}

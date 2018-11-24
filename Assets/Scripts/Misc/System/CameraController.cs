using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float Speed;
	public float EdgeRadius;
	public bool EdgeScrolling;
	public bool KeyScrolling;

	[Header("Zoom Speed")]
	public float ZoomSpeed_Position;
	public float ZoomSpeed_Rotation;

	[Header("Zoom Limit")]
	public Vector2 ZoomLimit_Position;		//x - min, y - max
	public Vector2 ZoomLimit_Rotation;		//x - min, y - max

	private Camera TargetCamera;
	private Vector2 Move;

	// Use this for initialization
	void Start () {
		TargetCamera = Camera.main;	
	}
	
	// Update is called once per frame
	void Update () {
		Move = Vector2.zero;
		if(EdgeScrolling){
			SetMove_Edge();
		}
		
		if(KeyScrolling){
			SetMove_Key();
		}

		Zoom();

		transform.Translate(new Vector3(Move.x * Speed, 0, Move.y * Speed), Space.World);
		
	}

	void SetMove_Edge(){
		if(Input.mousePosition.x > Screen.width - EdgeRadius){
			Move.x = 1;
		}else if(Input.mousePosition.x < EdgeRadius){
			Move.x = -1;
		}else{
			Move.x = 0;
		}

		if(Input.mousePosition.y > Screen.height - EdgeRadius){
			Move.y = 1;
		}else if(Input.mousePosition.y < EdgeRadius){
			Move.y = -1;
		}else{
			Move.y = 0;
		}
	}

	void SetMove_Key(){
		if(Input.GetAxis("Horizontal") != 0){
			Move.x = Input.GetAxis("Horizontal");
		}

		if(Input.GetAxis("Vertical") != 0){
			Move.y = Input.GetAxis("Vertical");
		}
	}

	void Zoom(){
		//Set position
		if(Input.mouseScrollDelta.y > 0){
			if(transform.position.y < ZoomLimit_Position.y){
				transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * ZoomSpeed_Position, 0));
			}
		}else{
			if(transform.position.y > ZoomLimit_Position.x){
				transform.Translate(new Vector3(0, Input.mouseScrollDelta.y * ZoomSpeed_Position, 0));
			}
		}
	

		//Set rotation
		if(Input.mouseScrollDelta.y > 0){
			if(transform.rotation.eulerAngles.x < ZoomLimit_Rotation.y){
				transform.Rotate(new Vector3(Input.mouseScrollDelta.y * ZoomSpeed_Rotation, 0, 0));
			}
		}else{
			if(transform.rotation.eulerAngles.x > ZoomLimit_Rotation.x){
				transform.Rotate(new Vector3(Input.mouseScrollDelta.y * ZoomSpeed_Rotation, 0, 0));
			}
		}

			
	}
}

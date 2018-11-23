using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float Speed;
	public float EdgeRadius;

	private Camera TargetCamera;
	private Vector2 Move;

	// Use this for initialization
	void Start () {
		TargetCamera = Camera.main;	
	}
	
	// Update is called once per frame
	void Update () {
		SetMove();
		//transform.Translate(transform.right * Move * Speed);
		//transform.Translate(new Vector3(Move.y * Speed, 0, -Move.x * Speed), Space.World);
		
	}

	void SetMove(){
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
}

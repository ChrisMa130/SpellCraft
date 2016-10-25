using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class CubeScript : MonoBehaviour {

	private Vector3 manipulationPreviousPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void PerformManipulationStart(Vector3 position)
	{
		//设置初始位置
		manipulationPreviousPosition = position;
	}

	void PerformManipulationUpdate(Vector3 position)
	{
		//if (GestureManager.Instance.IsManipulating)
		//{
			//计算相对位移，然后更新物体的位置   
			Vector3 moveVector = Vector3.zero;
			moveVector = position - manipulationPreviousPosition;
			manipulationPreviousPosition = position;
			transform.position += moveVector;
		//}
	}

	private void OnTap()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	private void OnDoubleTap()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
	}

	//新增改变物体颜色的方法，当收到改变颜色的指令，且凝视射线投射到该物体上时，修改当前物体颜色
	public void ChangeColor(string color)
	{
		if (GazeManager.Instance.FocusedObject == gameObject)
		{
			switch (color)
			{
			case "red":
				gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
				break;
			case "yellow":
				gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
				break;
			case "blue":
				gameObject.GetComponent<MeshRenderer> ().material.color = Color.blue;
				break;
			default:
				break;
			}

		}
	}
}
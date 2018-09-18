using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
		if(Time.timeScale == 0){Time.timeScale = 1;}
		if(sceneIndex == 0){Cursor.visible = true;}
		
		
		SceneManager.LoadScene (sceneIndex);
	}
}

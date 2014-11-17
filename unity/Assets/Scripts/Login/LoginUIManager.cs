using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginUIManager : UIManager
{
	// gameobject
	private GameObject facebookUI;
	// component
	private Register register;

	public override void Awake ()
	{
		BgmType = Bgm.NONE;
		BgmName = string.Empty;
		
		IsCache = false;
	}
	public override void Start ()
	{
		facebookUI = GameObject.Find (Config.FACEBOOK);
		register = Register.Instance ();
	}

	void Update ()
	{
		if (FB.IsLoggedIn) {
			facebookUI.SetActive (false);
		} else {
			facebookUI.SetActive (true);
		}
	}
	
	private void LoginCallback (FBResult result)
	{
		if (result.Error != null) {
		// "Error Response:\n" + result.Error;
			Debug.Log ("Error Response:\n" + result.Error);
		// debugText = result.Error;
		} else if (!FB.IsLoggedIn) {
		// "Login cancelled by Player";
			Debug.Log ("Login cancelled by Player");
		} else {
		// "Login was successful!";
			Debug.Log ("Login was successful!");
			
			gameObject.GetComponent<HttpComponent> ().Login (0);
		}
	}

	public void GameStart ()
	{
		int temp = register.GetMyPage ();
		if (temp < 0) {
			temp = 0;
		}

		temp = temp / Config.STAGE_COLOR_COUNT;

		SSSceneManager.Instance.Screen (Config.MYPAGE + temp.ToString ());
//		SSSceneManager.Instance.GoHome ();
	}
	
	public void FacebookLogin ()
	{
		if (!FB.IsLoggedIn) {
			FB.Login ("email, publish_actions", LoginCallback);
		}
	}

	public void Setting ()
	{
		SSSceneManager.Instance.PopUp (Config.SETTING);
	}
}



//
//  OpenAdAdapter.cs
//  OpenAdAdapter
//
//  Created by Artem Babenko on 7/14/15.
//  Copyright (c) 2015 Artem Babenko. All rights reserved.
//


using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class OpenAdAdapter : MonoBehaviour {

	public Canvas canvas;
	public UnityEngine.UI.Text text1;
	void Start(){
		//canvas = (Canvas)GetComponent ("Canvas");
		CmdInit ();

	}
	
	int second;
	
	void Update(){
		//Camera.current.rect = new Rect (0.01f, 0.3f, 0.9f, 0.6f);
		
		int second1 = DateTime.Now.Second;
		if (second == second1) {
			return;
		}
		
		second = second1;
		
		
		
		int ibh = OpenAdAdapter.GetBannerHeight ();
		float bh = (float)ibh;
		float sh = (float)Screen.height;
		
		//bh += (UnityEngine.Random.value * 0.3f - 0.15f) * sh;
		
		if (Camera.main == null) {
			Debug.Log("No camera");
		} else {
			if (bh == 0) {
				Camera.main.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f);
			} else if (bh > 0) {
				Camera.main.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f - bh / sh);
				//				canvas.pixelRect = new Rect (0.0f, 0.0f, 1.0f, 1.0f - bh / sh);
				//canvas.worldCamera.rect = new Rect (0.0f, 0.0f, 1.0f, 1.0f - bh / sh);
				//				canvas.transform.Translate(new Vector3(0.0f, bh, 0.0f));
			} else {
				Camera.main.rect = new Rect (0.0f, - bh / sh, 1.0f, 1.0f + bh / sh);
				//				canvas.pixelRect = new Rect (0.0f, - bh / sh, 1.0f, 1.0f + bh / sh);
				//canvas.worldCamera.rect = new Rect (0.0f, - bh / sh, 1.0f, 1.0f + bh / sh);
				//				canvas.transform.Translate(new Vector3(0.0f, bh, 0.0f));
			}
		}
		
	}
	
	
	
	
	
	public void CmdInit(){
		//  https://raw.githubusercontent.com/sample-data/oad1/master/ios-redir.json
		
		#if UNITY_IPHONE
		OpenAdAdapter.Init ("https://raw.githubusercontent.com/sample-data/oad1/master/ios-redir.json");
		#endif
		#if UNITY_ANDROID
		OpenAdAdapter.Init ("https://raw.githubusercontent.com/sample-data/oad1/master/android-redirect.json");
		#endif
		
	}
	public void CmdShowTopBanner(){
		OpenAdAdapter.ShowTopBanner ();
	}
	public void CmdShowBottomBanner(){
		OpenAdAdapter.ShowBottomBanner ();
	}
	public void CmdHideBanner(){
		OpenAdAdapter.HideBanner ();
	}
	public void CmdFullscreen(){
		OpenAdAdapter.Fullscreen ();
	}
	public void CmdVideo(){
		OpenAdAdapter.Video ();
	}
	public void CmdRewarded(){
		OpenAdAdapter.Rewarded ();
	}
	public void CmdCheckReward(){
		string s1 = OpenAdAdapter.CheckReward ();
		if (text1 == null) {
			return;
		}
		if (s1 == null) {
			text1.text = "nUll";
			return;
		}
		float amount = -1.0f;
		string network = null;
		string currency = null;
		bool ok = false;
		
		string[] ss = s1.Split (new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
		foreach (string s2 in ss) {
			if(s2 == null) continue;
			string[] ss2 = s2.Split(new char[]{'\t'}, StringSplitOptions.RemoveEmptyEntries);
			if (ss2 == null) continue;
			if (ss2.Length < 2) continue;
			if (ss2[0] == "amount"){
				ok = Single.TryParse(ss2[1], out amount);
			}
			if (ss2[0] == "network"){
				network = ss2[1];
			}
			if (ss2[0] == "currency"){
				currency = ss2[1];
			}
		}
		
		text1.text = "a: " + amount + " n: " + network + " c: " + currency + " ok: " + ok;
		
	}

	//extern "C" void MySDKFooBarCFunction();

	#if UNITY_IPHONE && !UNITY_EDITOR

	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_Init(string url);


	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_ShowTopBanner();
	
	
	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_ShowBottomBanner();
	
	
	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_HideBanner();
	
	
	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_Fullscreen();
	
	
	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_Video();
	
	
	[DllImport ("__Internal")]
	private static extern void UnityShimOAD_Rewarded();
	
	
	[DllImport ("__Internal")]
	private static extern string UnityShimOAD_CheckReward();
	
	[DllImport ("__Internal")]
	private static extern int UnityShimOAD_GetBannerHeightInPixels();

	[DllImport ("__Internal")]
	private static extern bool UnityShimOAD_HasReward();


#endif


	public static void Init(string url){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_Init (url);
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		if(activityClass != null){
			AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
			if (oadClass != null){
				oadClass.CallStatic("initFromUrl", activity, url);
			}
		}
		#endif
	}
	
	public static void ShowTopBanner(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_ShowTopBanner ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("showTopBanner", null);
		}
		#endif
	}
	
	
	public static void ShowBottomBanner(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_ShowBottomBanner ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("showBottomBanner", null);
		}
		#endif
	}
	
	
	public static void HideBanner(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_HideBanner ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("hideBanner");
		}
		#endif
	}


	public static void Fullscreen(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_Fullscreen ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("showFullscreen", null);
		}
		#endif
	}
	
	public static void Video(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_Video ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("showVideo", null);
		}
		#endif
	}
	
	public static void Rewarded(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		UnityShimOAD_Rewarded ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			oadClass.CallStatic("showRewarded", null);
		}
		#endif
	}

	public static bool HasReward(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		return UnityShimOAD_HasReward ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){
			
			bool b1 = oadClass.CallStatic<bool>("hasReward");
			return b1;

		}
		#endif
		return false;
	}
	
	public static string CheckReward(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		return UnityShimOAD_CheckReward ();
		#endif
		#if UNITY_ANDROID
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){

			bool b1 = oadClass.CallStatic<bool>("hasReward");

			if(!b1){
				return null;
			}
			
			
			AndroidJavaClass rwdClass = new AndroidJavaClass("com.openadadapter.Reward");
			if(rwdClass != null){
				AndroidJavaObject r1 = oadClass.CallStatic<AndroidJavaObject>("fetchReward");
				if (r1.GetRawObject().ToInt32() == 0){
					return null;
				}
				//		float height = oadClass.CallStatic<float>("getBannerHeightInPoints");
				float amount = r1.Call<float>("getAmount");
				string network = r1.Call<string>("getNetwork");
				string currency = r1.Call<string>("getCurrency");

				return "amount\t" + amount + "\nnetwork\t" + network + "\ncurrency\t" + currency;
			}
		}
		#endif
		return null;
	}
	
	public static int GetBannerHeight(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		return UnityShimOAD_GetBannerHeightInPixels ();
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass oadClass = new AndroidJavaClass("com.openadadapter.OpenAdAdapter");
		if (oadClass != null){

				float height = oadClass.CallStatic<float>("getBannerHeightInPixels");
	//		float height = oadClass.CallStatic<float>("getBannerHeightInPoints");
			return (int) height;
		}
		#endif
		return 0;
	}

}

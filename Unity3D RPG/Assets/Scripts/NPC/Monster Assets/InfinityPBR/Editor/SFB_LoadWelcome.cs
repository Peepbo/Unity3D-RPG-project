using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class Startup {
	static Startup()
	{ 
		if (EditorApplication.timeSinceStartup < 10) {
			var loadedCount = EditorPrefs.GetInt("SFB_LoadedCount");
			EditorPrefs.SetInt("SFB_LoadedCount", loadedCount + 1);
			Debug.Log ("Don't forget to download the bonus content from my site!");
			Debug.Log ("http://www.infinitypbr.com - Please Register, Review & Rate!\nDelete Assets/SFBayStudios/Editor/SFB_LoadWelcome.cs to stop this message.");
			Debug.Log ("Visit www.InfinityPBR.com to register your asset & get access to\nmaterials/textures, bonus downloads, pre-release content & support.");
			Debug.Log ("Please review & rate your purchase.  Your positive review let's\nothers know they can trust our work.  We can't thank you enough!");
			if (EditorPrefs.GetInt("SFB_Welcome2") != 1 && EditorPrefs.GetInt("SFB_LoadedCount2") % 7 == 0 )
			{
				var option = EditorUtility.DisplayDialogComplex(
					"www.InfinityPBR.com: Please Register, Review & Rate!",
					"Your Asset Store purchase contains more than what's included!  Due to differences between 2017.x and 2018.x, and in an effort to keep the download size down, all customization files and other bonus content (Concept Art etc), can be downloaded from my site.\n\nPlease review & rate your purchase.  Your review & rating helps others know how high quality our work is!",
					"Go to InfinityPBR.com",
					"Remind Me Later",
					"Don't Show Again");
				switch (option) {
				case 0:
					Application.OpenURL ("http://www.InfinityPBR.com/");
					break;
				case 1:
					break;
				case 2:
					EditorPrefs.SetInt("SFB_Welcome2", 1);
					break;
				default:
					Debug.LogError("Unrecognized option.");
					break;
				}
			}
		}
	}
}

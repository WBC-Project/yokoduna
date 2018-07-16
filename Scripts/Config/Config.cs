using UnityEngine;

namespace WBCProject {
	/// <summary>
	/// API Configration
	/// </summary>
	[System.Serializable, CreateAssetMenu(menuName="Yokoduna/Settings")]
	public class Config : ScriptableObject {
		public string apikey;
		public string productkey;
		[HideInInspector] public string baseURL = "https://yokoduna.axluse.net/";
	}
}

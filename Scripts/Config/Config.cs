using UnityEngine;

namespace  Yokoduna {
	[System.Serializable, CreateAssetMenu(menuName="Yokoduna/Settings")]
	public class Config : ScriptableObject {
		public string apikey;
		public string productkey;
		[HideInInspector] public string baseURL = "http://localhost/yokoduna/public/";
	}
}

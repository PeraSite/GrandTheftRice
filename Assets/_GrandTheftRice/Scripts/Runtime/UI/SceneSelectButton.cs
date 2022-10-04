using UnityEngine;

namespace GrandTheftRice.UI {
	public class SceneSelectButton : MonoBehaviour {
		public void GoMainMenu() {
			GameModeManager.Instance.HandleStartRequested(GameModeManager.Instance.mainMenuMode);
		}
	}
}

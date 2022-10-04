using System.Collections;
using PeraCore.Runtime;
using PixelCrushers;
using UnityEngine.SceneManagement;

namespace GrandTheftRice {
	public class MainMenuGameMode : GameModeBase {
		[SceneSelector]
		public string mainMenuScene;

		public override IEnumerator OnStart() {
			yield return SceneManager.LoadSceneAsync(mainMenuScene);
		}

		public override IEnumerator OnEditorStart() {
			yield return null;
		}

		public override IEnumerator OnEnd() {
			yield return null;
		}
	}
}

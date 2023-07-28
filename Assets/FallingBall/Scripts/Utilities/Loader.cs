using UnityEngine.SceneManagement;

namespace FallingBall
{
    public static class Loader
    {
        public enum Scene
        {
            MainmenuScene,
            GameplayScene,
            GameoverScene,
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene, System.Action afterLoadScene = null)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Loader.targetScene.ToString());
        }
    }
}

namespace Mud_game
{
    class Scene : IRenderer //모든 씬의 부모
    {
        public Scene nextScene = null;
        public bool isGameContinue = true;
        public virtual void Show()
        {

        }

        public void ChangeScene(Scene newScene) //씬 바꾸는 함수
        {
            nextScene = newScene;
        }

        public void ExitGame() // 게임 멈추는 함수
        {
            isGameContinue = false;
        }
    }
}

using Mud_game;

class SubScene : Scene, IRenderer
{
    public override void Show()
    {
        Console.WriteLine("서브");
        Console.ReadKey();
    }
}//예시로 만든 클래스

class Program
{
    static void Main(string[] args)
    {
        Scene currentScene = new MainScene();//시작씬

        while (currentScene.isGameContinue)
        {
            Console.Clear();
            currentScene.Show();

            if (currentScene.nextScene != null)
            {
                //만약에 현재의 씬에서 다른씬으로 넘어가는 조건이 만족이된다면 다음씬으로 이동
                currentScene = currentScene.nextScene;
            }

        }

        Console.WriteLine("게임이 종료되었습니다.");
    }
}

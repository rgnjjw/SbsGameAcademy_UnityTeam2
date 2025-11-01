namespace Mud_game
{
    class MainScene : Scene
    {
        public override void Show() //IRender의 Show 함수 구현
        {
            //여기에 원하는 텍스트 넣으면 됨
            Console.WriteLine("메인");
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Spacebar) //넘어가기 위한 조건
            {
                ChangeScene(new SubScene()); //넘어가기 원하는 씬 넣기
            }
            else if (key.Key == ConsoleKey.Escape) //넘어가기 위한 조건
            {
                ExitGame(); //넘어가기 원하는 씬 넣기
            }
        }
    }
}

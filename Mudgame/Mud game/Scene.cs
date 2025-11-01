using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud_game
{

    public class Scene //모든 씬의 부모
    {
        public Scene nextScene = null;
        public bool isGameContinue = true;

        public void ChangeScene(Scene newScene) //씬 바꾸는 함수
        {
            nextScene = newScene;
        }

        public void ExitGame() // 게임 멈추는 함수
        {
            isGameContinue = false;
        }
    }

   //public class MainScene : Scene, IRenderer
   // {
   //     public void Show() //IRender의 Show 함수 구현
   //     {
   //         //여기에 원하는 텍스트 넣으면 됨
   //         Console.WriteLine("메인");
   //         var key = Console.ReadKey();
   //         Console.WriteLine();

   //         if (key.Key == ConsoleKey.Spacebar) //넘어가기 위한 조건
   //         {
   //             ChangeScene(new SubScene()); //넘어가기 원하는 씬 넣기
   //         }
   //         else if (key.Key == ConsoleKey.Escape) //넘어가기 위한 조건
   //         {
   //             ExitGame(); //넘어가기 원하는 씬 넣기s
   //         }
   //     }
   // }

   // public class SubScene : Scene, IRenderer
   // {
   //     public void Show()
   //     {
   //         Console.WriteLine("서브");
   //         Console.ReadKey();
   //     }
   // }
    //예시
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Scene currentScene = new MainScene();//시작씬

    //        while (currentScene.isGameContinue)
    //        {
    //            Console.Clear();

    //            if (currentScene is IRenderer render)
    //            {
    //                //현재 씬이 IRender라는 인터페이스를 상속받고 있다면 reder라는 currentScene의 복사본을 만든뒤 Show()함수 실행
    //                render.Show();
    //            }
    //            else
    //            {
    //                Console.WriteLine("화면에 띄울것이 없는 씬입니다");
    //            }

    //            if (currentScene.nextScene != null)
    //            {
    //                //만약에 현재의 씬에서 다른씬으로 넘어가는 조건이 만족이된다면 다음씬으로 이동
    //                currentScene = currentScene.nextScene;
    //            }

    //        }

    //        Console.WriteLine("게임이 종료되었습니다.");
    //    }
    //}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Mud_game
{
    // --- 1. 캐릭터 선택 로직 핵심 클래스 ---
    /// 캐릭터 생성 전체 과정을 관리
    public class CharacterCreator
    {
        /*internal class Program Main
        {
            static void Main(string[] args)
            {
                // 1. 캐릭터 생성 관리 클래스 호출
                CharacterCreator creator = new CharacterCreator();

                // 2. 캐릭터 생성 프로세스 실행
                Player player = creator.CreateCharacterProcess();

                // 3. 생성된 플레이어 정보 확인(예시)
                Console.Clear();
                Console.WriteLine("===== 캐릭터 생성 완료 =====");
                Console.WriteLine($"이름: {player.Name}");
                Console.WriteLine($"직업: {player.Job.JobName}");
                Console.WriteLine($"체력: {player.Job.Stats.Hp}");
                Console.WriteLine($"공격력: {player.Job.Stats.Atk}");
                Console.WriteLine($"마나: {player.Job.Stats.Mp}");
                Console.WriteLine("===========================");

                // 4. 다음 씬(예: 마을, 전투 등)으로 넘어가는 로직 위치
                // GameManager.LoadScene("TownScene");  <-- 이런 식으로 나중에 연결
            }
        }*/
        // 선택 가능한 직업 목록
        private List<ICharacterJob> availableJobs;
        public CharacterCreator()
        {
            // 직업 목록 초기화
            availableJobs = new List<ICharacterJob>
            {
                new Swordsman(),
                new Mage(),
                new Assassin(),
                new Archer()
            };
        }

        /// 캐릭터 생성 프로세스를 시작하고, 완료된 Player 객체 반환
        /// 이 메서드가 외부(GameManager 등)에서 호출됨
        public Player CreateCharacterProcess()
        {
            // --- 씬에 필요한 콘솔 설정 ---
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Clear();

            // --- 핵심 프로세스 실행 ---
            // 플레이어 닉네임 설정 UI 및 로직
            string playerName = GetPlayerName();
            // 직업 선택 UI 및 로직
            ICharacterJob selectedJob = ChooseJob();

            // 최종 Player 객체 생성
            Player newPlayer = new Player
            {
                Name = playerName,
                Job = selectedJob
            };

            // --- 씬 종료 및 정리 ---
            Console.Clear();
            Console.CursorVisible = true;

            return newPlayer;
        }

        private string GetPlayerName()
        {
            StringBuilder nameBuilder = new StringBuilder();

            // 가상 키보드 레이아웃
            char[,] keyboard = new char[,]
            {
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K' },
                { 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' },
                { 'W', 'X', 'Y', 'Z', ' ', ' ', '<', ' ', ' ', ' ', '\r' } // '<' = Backspace, '\r' = Enter
            };

            int cursorX = 0;
            int cursorY = 0;
            int keyboardWidth = keyboard.GetLength(1);
            int keyboardHeight = keyboard.GetLength(0);

            while (true)
            {
                Console.Clear();
                ConsoleHelper.DrawText(2, 2, "---플레이어의 닉네임(이름)을 지정해 주세요---");

                ConsoleHelper.DrawBox(2, 4, 30, 3);
                ConsoleHelper.DrawText(4, 5, nameBuilder.ToString());

                // 키보드 그리기
                for (int y = 0; y < keyboardHeight; y++)
                {
                    for (int x = 0; x < keyboardWidth; x++)
                    {
                        int drawX = 4 + (x * 3);
                        int drawY = 8 + (y * 2);
                        char keyChar = keyboard[y, x];

                        string displayChar = keyChar.ToString();
                        if (keyChar == '<') displayChar = "BS";  // Backspace
                        else if (keyChar == '\r') displayChar = "OK"; // Enter 키를 'OK'로 표시

                        if (x == cursorX && y == cursorY)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            ConsoleHelper.DrawText(drawX, drawY, displayChar);
                            Console.ResetColor();
                        }
                        else if (keyChar != ' ')
                        {
                            ConsoleHelper.DrawText(drawX, drawY, displayChar);
                        }
                    }
                }
                ConsoleHelper.DrawText(2, 15, "화살표로 움직이고 Z 키를 눌러 선택 ('OK'에서 Z키로 완료)");

                // 키 입력 받기
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursorY = (cursorY > 0) ? cursorY - 1 : keyboardHeight - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        cursorY = (cursorY < keyboardHeight - 1) ? cursorY + 1 : 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        cursorX = (cursorX > 0) ? cursorX - 1 : keyboardWidth - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        cursorX = (cursorX < keyboardWidth - 1) ? cursorX + 1 : 0;
                        break;
                    case ConsoleKey.Z:
                        char selectedChar = keyboard[cursorY, cursorX];
                        if (selectedChar == '\r') // Enter (OK)
                        {
                            if (nameBuilder.Length > 0)
                                return nameBuilder.ToString();
                            // 이름이 비어있으면 무시
                        }
                        else if (selectedChar == '<') // Backspace
                        {
                            if (nameBuilder.Length > 0)
                                nameBuilder.Length--;
                        }
                        else if (selectedChar != ' ' && nameBuilder.Length < 27) // 최대 이름 제한
                        {
                            nameBuilder.Append(selectedChar);
                        }
                        break;
                }
            }
        }

        private ICharacterJob ChooseJob()
        {
            int selectedJobIndex = 0;
            // --- 직업 목록 브라우징 ---
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < availableJobs.Count; i++)
                {
                    // "사진" 영역
                    int photoX = 5 + (i * 18);
                    ConsoleHelper.DrawBox(photoX, 3, 16, 8);
                    ConsoleHelper.DrawText(photoX + 6, 7, "사진");

                    // "직업명" 영역
                    int nameX = 5 + (i * 18) + (16 - availableJobs[i].JobName.Length) / 2;
                    int nameY = 12;

                    if (i == selectedJobIndex)
                    {
                        // 하이라이트
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        ConsoleHelper.DrawText(nameX - 1, nameY, $"[ {availableJobs[i].JobName} ]");
                        Console.ResetColor();
                    }
                    else
                    {
                        ConsoleHelper.DrawText(nameX, nameY, availableJobs[i].JobName);
                    }
                }

                // 단순 텍스트 표시
                int selectButtonX = 35;
                int selectButtonY = 16;
                ConsoleHelper.DrawText(selectButtonX, selectButtonY, "선택하기");

                // 키 입력 처리
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        selectedJobIndex = (selectedJobIndex > 0) ? selectedJobIndex - 1 : availableJobs.Count - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        selectedJobIndex = (selectedJobIndex < availableJobs.Count - 1) ? selectedJobIndex + 1 : 0;
                        break;
                    case ConsoleKey.Z:
                        bool confirmed = ShowJobConfirmation(availableJobs[selectedJobIndex]);

                        if (confirmed)
                        {
                            // 사용자가 Scene 2에서 '선택하기'를 누름
                            return availableJobs[selectedJobIndex];
                        }
                        break;
                }
            }
        }

        /// Scene 2: 직업 정보 확인 및 최종 선택
        private bool ShowJobConfirmation(ICharacterJob job)
        {
            bool onConfirmButton = true; // true: 선택하기, false: 뒤로가기

            while (true)
            {
                Console.Clear();

                // 선택된 직업 정보 표시
                job.DrawPortraitAndStats(5, 7);

                // [선택하기] [뒤로가기] 버튼 그리기
                int buttonY = 22;
                int confirmX = 25;
                int backX = 45;

                // --- 1. '선택하기' 버튼 그리기---
                if (onConfirmButton)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    ConsoleHelper.DrawText(confirmX - 2, buttonY, "[ 선택하기 ]");
                    Console.ResetColor();
                }
                else
                {
                    ConsoleHelper.DrawText(confirmX - 2, buttonY, "[ 선택하기 ]");
                }


                // --- 2. '뒤로가기' 버튼 그리기---
                if (!onConfirmButton)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    ConsoleHelper.DrawText(backX - 2, buttonY, "[ 뒤로가기 ]");
                    Console.ResetColor();
                }
                else
                {
                    ConsoleHelper.DrawText(backX - 2, buttonY, "[ 뒤로가기 ]");
                }

                // 3. 키 입력 처리
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (!onConfirmButton) onConfirmButton = true;
                        break;
                    case ConsoleKey.RightArrow:
                        if (onConfirmButton) onConfirmButton = false;
                        break;
                    case ConsoleKey.Z:
                        if (onConfirmButton)
                        {
                            return true; // 선택 완료
                        }
                        else
                        {
                            return false; // 뒤로가기
                        }
                }
            }
        }
    }

    // --- 데이터 구조 (Interfaces 및 Classes) ---
    /// 최종 생성된 플레이어 정보를 담을 클래스
    public class Player
    {
        public string Name { get; set; }
        public ICharacterJob Job { get; set; }
    }

    interface ICharacter
    {
        void writename();
        void SelectCharacter();
    }

    /// 직업의 공통 속성(스탯, 이름, 정보 표시)을 정의하는 인터페이스
    public interface ICharacterJob
    {
        string JobName { get; }
        stat Stats { get; }

        /// 콘솔에 직업 정보(사진틀, 스탯) 그리기
        void DrawPortraitAndStats(int x, int y);
    }

    public class stat
    {
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Mp { get; set; }
        public stat(int hp, int atk, int mp)
        {
            this.Hp = hp;
            this.Atk = atk;
            this.Mp = mp;
        }
    }

    // --- 직업 구현 클래스 ---
    public class Swordsman : ICharacterJob
    {
        public string JobName => "검사";
        public stat Stats => new stat(5, 3, 3); // 스탯 (★★★★★, ★★★☆☆, ★★★☆☆)

        public void DrawPortraitAndStats(int x, int y)
        {
            ConsoleHelper.DrawBox(x, y, 24, 10);
            // 이미지 신 메니저 호출 자리
            ConsoleHelper.DrawText(x + 7, y + 5, "검사 캐릭터 사진");

            int statX = x + 30;
            ConsoleHelper.DrawText(statX, y, "스탯      ------------------");
            ConsoleHelper.DrawText(statX, y + 3, "체력      ");
            ConsoleHelper.DrawStars(statX + 10, y + 3, Stats.Hp, 5);
            ConsoleHelper.DrawText(statX, y + 5, "데미지    ");
            ConsoleHelper.DrawStars(statX + 10, y + 5, Stats.Atk, 5);
            ConsoleHelper.DrawText(statX, y + 7, "마나      ");
            ConsoleHelper.DrawStars(statX + 10, y + 7, Stats.Mp, 5);
        }
    }

    public class Mage : ICharacterJob
    {
        public string JobName => "마법사";
        public stat Stats => new stat(2, 5, 4); // 스탯 (★★☆☆☆, ★★★★★, ★★★★☆)

        public void DrawPortraitAndStats(int x, int y)
        {
            ConsoleHelper.DrawBox(x, y, 24, 10);
            // 이미지 신 메니저 호출 자리
            ConsoleHelper.DrawText(x + 7, y + 5, "마법사 캐릭터 사진");

            int statX = x + 30;
            ConsoleHelper.DrawText(statX, y, "스탯      ------------------");
            ConsoleHelper.DrawText(statX, y + 3, "체력      ");
            ConsoleHelper.DrawStars(statX + 10, y + 3, Stats.Hp, 5);
            ConsoleHelper.DrawText(statX, y + 5, "데미지    ");
            ConsoleHelper.DrawStars(statX + 10, y + 5, Stats.Atk, 5);
            ConsoleHelper.DrawText(statX, y + 7, "마나      ");
            ConsoleHelper.DrawStars(statX + 10, y + 7, Stats.Mp, 5);
        }
    }

    public class Assassin : ICharacterJob
    {
        public string JobName => "암살자";
        public stat Stats => new stat(2, 3, 3); // 스탯 (★★☆☆☆, ★★★☆☆, ★★★☆☆)

        public void DrawPortraitAndStats(int x, int y)
        {
            ConsoleHelper.DrawBox(x, y, 24, 10);
            // 이미지 신 메니저 호출 자리
            ConsoleHelper.DrawText(x + 7, y + 5, "암살자 캐릭터 사진");

            int statX = x + 30;
            ConsoleHelper.DrawText(statX, y, "스탯      ------------------");
            ConsoleHelper.DrawText(statX, y + 3, "체력      ");
            ConsoleHelper.DrawStars(statX + 10, y + 3, Stats.Hp, 5);
            ConsoleHelper.DrawText(statX, y + 5, "데미지    ");
            ConsoleHelper.DrawStars(statX + 10, y + 5, Stats.Atk, 5);
            ConsoleHelper.DrawText(statX, y + 7, "마나      ");
            ConsoleHelper.DrawStars(statX + 10, y + 7, Stats.Mp, 5);
        }
    }

    public class Archer : ICharacterJob
    {
        public string JobName => "궁수";
        public stat Stats => new stat(3, 4, 2); // 스탯 (★★★☆☆, ★★★★☆, ★★☆☆☆)

        public void DrawPortraitAndStats(int x, int y)
        {
            ConsoleHelper.DrawBox(x, y, 24, 10);
            // 이미지 신 메니저 호출 자리
            ConsoleHelper.DrawText(x + 7, y + 5, "궁수 캐릭터 사진");

            int statX = x + 30;
            ConsoleHelper.DrawText(statX, y, "스탯      ------------------");
            ConsoleHelper.DrawText(statX, y + 3, "체력      ");
            ConsoleHelper.DrawStars(statX + 10, y + 3, Stats.Hp, 5);
            ConsoleHelper.DrawText(statX, y + 5, "데미지    ");
            ConsoleHelper.DrawStars(statX + 10, y + 5, Stats.Atk, 5);
            ConsoleHelper.DrawText(statX, y + 7, "마나      ");
            ConsoleHelper.DrawStars(statX + 10, y + 7, Stats.Mp, 5);
        }
    }

    // --- UI 드로잉 헬퍼 클래스 ---

    /// 콘솔에 UI 요소를 그리는 static 도우미 클래스
    /// (씬 매니저가 이와 유사한 기능을 담당)
    public static class ConsoleHelper
    {
        
        /// 텍스트 그리기
        
        public static void DrawText(int x, int y, string text)
        {
            if (x < 0 || y < 0) return; // 버퍼 범위 밖 그리기 방지
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
            catch (ArgumentOutOfRangeException)
            {
                // 콘솔 크기 조절 등으로 인한 예외 처리
            }
        }

        /// 지정된 위치에 상자 그리기
        public static void DrawBox(int x, int y, int width, int height)
        {
            // 상단
            DrawText(x, y, "┌" + new string('─', width - 2) + "┐");
            // 중단
            for (int i = 1; i < height - 1; i++)
            {
                DrawText(x, y + i, "│" + new string(' ', width - 2) + "│");
            }
            // 하단
            DrawText(x, y + height - 1, "└" + new string('─', width - 2) + "┘");
        }

        /// 스탯을 ★로 표시
        public static void DrawStars(int x, int y, int rating, int max)
        {
            string stars = "";
            stars += new string('★', rating);
            stars += new string('☆', max - rating);

            DrawText(x, y, "|");
            Console.ForegroundColor = ConsoleColor.Yellow;
            DrawText(x + 1, y, stars.Substring(0, rating));
            Console.ForegroundColor = ConsoleColor.Gray;
            DrawText(x + 1 + rating, y, stars.Substring(rating));
            Console.ResetColor();
            DrawText(x + 1 + max, y, "|");
        }
    }
}

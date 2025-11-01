using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud_game
{
    class BattleScene : Scene, IRenderer
    {
        int playerX = 2, playerY = 10; // 플레이어 정보 위치
        int enemyX = 40, enemyY = 10;  // 적 정보 위치

        PlayerInfomation playerInfo;
        EnemyInfomation enemyInfo;
        public BattleScene(PlayerInfomation playerInfomation, EnemyInfomation enemyInfomaion)
        {
            playerInfo = playerInfomation;
            enemyInfo = enemyInfomaion;
        }
        public void Show()
        {

            Console.Clear();

            // 플레이어 정보 출력
            Console.SetCursorPosition(playerX, playerY);
            Console.Write($"이름: {playerInfo.name}");
            Console.SetCursorPosition(playerX, playerY + 1);
            Console.Write($"현재체력: {playerInfo.currentHp}");
            Console.SetCursorPosition(playerX, playerY + 2);
            Console.Write($"공격력: {playerInfo.attackPower}");
            Console.SetCursorPosition(playerX, playerY + 3);
            Console.Write($"방어력: {playerInfo.defencePower}");

            // 적 정보 출력
            Console.SetCursorPosition(enemyX, enemyY);
            Console.Write($"이름: {enemyInfo.name}");
            Console.SetCursorPosition(enemyX, enemyY + 1);
            Console.Write($"현재체력: {enemyInfo.hp}");
            Console.SetCursorPosition(enemyX, enemyY + 2);
            Console.Write($"공격력: {enemyInfo.attackPower}");
            Console.SetCursorPosition(enemyX, enemyY + 3);
            Console.Write($"방어력: {enemyInfo.defencePower}");
            
        }
        static void Main(string[] args)
        {
            PlayerInfomation exPlayerInfomation = new PlayerInfomation("Player", 100, 100, 10, 10);
            EnemyInfomation exEnemyInfomation = new EnemyInfomation(50, 1, 30, 5, 3, "Enemy");
            BattleScene exBattle = new BattleScene(exPlayerInfomation,exEnemyInfomation);
            exBattle.Show();
            
        }
    }
        }

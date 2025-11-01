using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud_game
{
    public class EnemyInfomation
    {
        public string name;

        public int hp;
        public int level;
        public int gold;
        public int attackPower;
        public int defencePower;

        public EnemyInfomation(int hp,int exp, int gold,int attackPower,int defencePower,string name)
        {
            this.hp = hp;
            this.level = exp;
            this.gold = gold;
            this.attackPower = attackPower;
            this.defencePower = defencePower;
            this.name = name;
        }
    }
    
}

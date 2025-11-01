using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud_game
{
    public class PlayerInfomation
    {
        public PlayerSkill[] playerSkills;

        public string name;
        public string job;

        public int maxHp;
        public int currentHp;
        public int attackPower;
        public int defencePower;

        public PlayerInfomation(string name,int maxHp,int currentHp,int attackPower,int defencePower)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.currentHp = currentHp;
            this.attackPower = attackPower;
        }
    }
}

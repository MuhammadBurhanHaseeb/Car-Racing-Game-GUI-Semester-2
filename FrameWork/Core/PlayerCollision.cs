using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
   public class PlayerCollision:ICollision
    {
       public  void performAction(Core.IGame game, GameObject source1, GameObject source2)
        {
            GameObject player;
            if(source1.OtherType == GameObjectName.player)
            {
                player = source1;
            }
            else
            {
                player = source2;
            }
            game.raisePlayerDieEvent(player.Pb);


        }
    }
}

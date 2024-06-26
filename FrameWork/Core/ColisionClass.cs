using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
   public  class ColisionClass
    {
        private GameObjectName a;
        private GameObjectName b;
        private ICollision actionBehave;
        public ColisionClass(GameObjectName x, GameObjectName y, ICollision action)
        {
            A = x;
            B = y;
            ActionBehave = action;
        }

        public GameObjectName A { get => a; set => a = value; }
        public GameObjectName B { get => b; set => b = value; }
        public ICollision ActionBehave { get => actionBehave; set => actionBehave = value; }
    }
}

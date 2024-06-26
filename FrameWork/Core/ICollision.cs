using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
   public  interface ICollision
    {
        void performAction(IGame game , GameObject source1, GameObject source2);
    }
}

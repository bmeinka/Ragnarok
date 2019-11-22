using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.Gameplay.Control
{
    interface IControlState
    {
        void Update(Controller parent);
    }
}

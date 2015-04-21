using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCsharp
{
    [Serializable]
    public class DataTransit
    {
        private Balle balle = null;
        private Boolean alive = true;
        public Balle BallePro { get { return balle; } set { balle = value; } }
        public Boolean Alive { get { return alive; } set { alive = value; } }
    }
}

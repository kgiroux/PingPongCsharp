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
        private Boolean timer = false;
        private Boolean reset = false;
        private String nameJoueurClient = "";
        private String nameJoueurServer = "";
        public Balle BallePro { get { return balle; } set { balle = value; } }
        public Boolean Alive { get { return alive; } set { alive = value; } }
        public Boolean Timer { get { return timer; } set { timer = value; } }
        public Boolean Reset { get { return reset; } set { reset = value; } }

        public String NameClient { get { return nameJoueurClient; } set { nameJoueurClient = value; } }

        public String NameServer { get { return nameJoueurServer; } set { nameJoueurServer = value; } }
    }
}

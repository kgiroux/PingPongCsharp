using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCsharp
{
    public sealed class SingletonDb
    {
        private static readonly object padlock = new object();
        private static ScoreResultEntities pongResult = null;

        public SingletonDb()
        {
        }

        public static ScoreResultEntities Instance
        {
            get
            {
                lock (padlock)
                {
                    if (pongResult == null)
                    {
                        pongResult = new ScoreResultEntities();
                    }
                    return pongResult;
                }
            }
        }
    }
}

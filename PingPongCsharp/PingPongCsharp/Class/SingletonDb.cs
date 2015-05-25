using PingPongCsharp.Bdd;
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
        private static ScoreResultEntitiesTest pongResult = null;
        /// <summary>
        /// Constructeur vide pour un singleton
        /// </summary>
        public SingletonDb()
        {
        }
        /// <summary>
        /// Permet de crée une unique instance de ScoreResultEntities;
        /// </summary>
        public static ScoreResultEntitiesTest Instance
        {
            get
            {
                lock (padlock)
                {
                    if (pongResult == null)
                    {
                        pongResult = new ScoreResultEntitiesTest();
                    }
                    return pongResult;
                }
            }
        }
    }
}

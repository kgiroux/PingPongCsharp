using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<author>
///Cyril LEFEBVRE & Kévin Giroux
///</author>
namespace PingPongCsharp
{
    public sealed class SingletonDb
    {
        private static readonly object padlock = new object();
        private static ScoreResultEntities pongResult = null;

        /// <summary>
        /// Constructeur vide pour un singleton
        /// </summary>
        public SingletonDb() {}

        /// <summary>
        /// Permet de crée une unique instance de ScoreResultEntities;
        /// </summary>
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

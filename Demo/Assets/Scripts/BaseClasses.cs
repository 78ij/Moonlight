using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unnamed.UnnamedGame
{
    public class PressState
    {
        public int duepressedtime = 0;
        public bool isexceeded = true;
        public bool isplayed = false;
        public bool istouched = false;
        public PressState(int _time)
        {
            duepressedtime = _time;
        }
    }
}
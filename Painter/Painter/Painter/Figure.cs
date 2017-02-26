﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Painter
{
    public abstract class Figure
    {
        protected Pen PenType;
        protected Graphics _Painter;

        public abstract void Draw();

        public Pen Pen
        {
            get { return PenType; }
            set
            {
                if (value != null)
                {
                    this.PenType = value;
                }
            }
        }
        
        public Graphics Painter
        {
            get { return _Painter; }
            set { _Painter = value; }
        }     
    }
}
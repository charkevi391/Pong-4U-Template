using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pong
{
    class Class1
    {
        public SolidBrush drawBrush;
        public int x, y, sizeX, sizeY;

        public Class1()
        {

        }

        public Class1(int _x, int _y, int _Xsize, int _Ysize)
        {
            x = _x;
            y = _y;
            sizeX = _Xsize;
            sizeY = _Ysize;
        }

        public Class1(SolidBrush _drawBrush,int _x, int _y, int _Xsize, int _Ysize)
        {
            x = _x;
            y = _y;
            sizeX = _Xsize;
            sizeY = _Ysize;
            drawBrush = _drawBrush;
        }

        public bool Collides(Rectangle rec)
        {
            Rectangle me = new Rectangle(x, y, sizeX, sizeY);
            
            if (me.IntersectsWith(rec))
            {
                return true;
            }
            return false;
        }

        public bool Collides(Class1 classObject)
        {
            Rectangle me = new Rectangle(x, y, sizeX, sizeY);
            Rectangle classRec = new Rectangle(classObject.x, classObject.y, classObject.sizeX, classObject.sizeY);

            if (me.IntersectsWith(classRec))
            {
                return true;
            }
            return false;
        }
    }
}

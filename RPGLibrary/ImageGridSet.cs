using System;
using System.Collections.Generic;
using System.Text;

namespace RPGLibrary
{
    public class ImageGridSet
    {
        string _name;
        int _positionGridX;
        int _positionGridY;

        public string Name { get => _name; set => _name = value; }
        public int PositionGridX { get => _positionGridX; set => _positionGridX = value; }
        public int PositionGridY { get => _positionGridY; set => _positionGridY = value; }
    }
}

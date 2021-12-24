using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public enum Chocolate
    {
        None,
        Bronze,
        Gold,
        Pink,
        Purple
    }

    public static class ChocolateColor
    {
        
        static Color Bronze = new Color(0.66f, 0.22f, 0.2f);
        static Color Gold = new Color(0.81f, 0.65f, 0.3f);
        static Color Pink = new Color(0.8f, 0.2f, 0.4f);
        static Color Purple = new Color(0.66f, 0.24f, 0.67f);

        public static Color GetChocolateColor(Chocolate chocolate)
        {
            switch (chocolate)
            {
                case Chocolate.None:
                    return Color.white;
                case Chocolate.Bronze:
                    return Bronze;
                case Chocolate.Gold:
                    return Gold;
                case Chocolate.Pink:
                    return Pink;
                case Chocolate.Purple:
                    return Purple;
                default:
                    return Color.white;
            }
        }
    }

    public enum Pattern
    {
        Empty,
        Daisy,
        Spotted,
        Star,
        Vertical,
        Zigzag,
        Zigzag1,
        Zigzag2,
        Zigzag3,
        Zigzag4,
        
    }
}
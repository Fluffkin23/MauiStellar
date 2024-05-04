using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStellar2.Model
{
    public class ZodiacSign
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Personality { get; set; }

        public string Image {  get; set; } // path to the image

        public string Color {  get; set; }

        public ZodiacSign(string name, string description, string personality,string image, string color)
        {
            Name = name;
            Description = description;
            Personality = personality;
            Image = image;
            Color = color;
        }
    }

}

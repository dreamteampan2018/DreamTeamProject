﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Models
{
    public class FindMyCoverModel
    {
    }

    public class Rootobject
    {
        public string status { get; set; }
        public Recognitionresult recognitionResult { get; set; }
    }

    public class Recognitionresult
    {
        public Line[] lines { get; set; }
    }

    public class Line
    {
        public int[] boundingBox { get; set; }
        public string text { get; set; }
        public Word[] words { get; set; }
    }

    public class Word
    {
        public int[] boundingBox { get; set; }
        public string text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScrumLearning
{
    public class Opinion
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public int Note { get; set; }
        public string Critic { get; set; }

        public Opinion(string title, int year, string director, int note, string critic)
        {
            Title = title;
            Year = year;
            Director = director;
            Note = note;
            Critic = critic;
        }

        public Opinion()
        {
        }
    }
}

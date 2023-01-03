using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; }
    }
}

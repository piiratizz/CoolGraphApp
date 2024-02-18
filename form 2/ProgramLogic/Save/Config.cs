using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolGraphicsApp.ProgramLogic.Save
{
    [Serializable]
    internal class Config
    {
        public string Title { get; set; }
        public string LineName { get; set; }
        public decimal LineWidth { get; set; }
        public string XAxisName { get; set; }
        public string YAxisName { get; set; }
        public Color LineColor { get; set; }
        public Color BgColor { get; set; }
    }
}

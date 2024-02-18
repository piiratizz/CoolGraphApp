using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolGraphicsApp.ProgramLogic.Save
{
    internal class ConfigBuilder
    {
        private Config _config;

        public ConfigBuilder(Config config) 
        {
            _config = config;
        }

        public ConfigBuilder SetTitle(string text) 
        {
            _config.Title = text;
            return this;
        }

        public ConfigBuilder SetLineName(string text)
        {
            _config.LineName = text;
            return this;
        }

        public ConfigBuilder SetLineWidth(decimal number)
        {
            _config.LineWidth = number;
            return this;
        }

        public ConfigBuilder SetXAxisName(string text)
        {
            _config.XAxisName = text;
            return this;
        }

        public ConfigBuilder SetYAxisName(string text)
        {
            _config.YAxisName = text;
            return this;
        }

        public ConfigBuilder SetLineColor(Color color)
        {
            _config.LineColor = color;
            return this;
        }

        public ConfigBuilder SetBgColor(Color color)
        {
            _config.BgColor = color;
            return this;
        }

        public Config GetConfig()
        {
            return _config;
        }
    }
}

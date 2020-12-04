using System;
using System.Collections.Generic;
using System.Text;
using NSController;

namespace NSView
{
    public interface IView
    {
        void Start();
        void PrintMessage(string message, int type);
        public Controller Controller { get; set; }
    }
}

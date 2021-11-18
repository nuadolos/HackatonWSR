using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WSR_2021.Model;

namespace WSR_2021.Utils
{
    class Transition
    {
        public static Frame MainFrame { get; set; }

        private static HackatonWSREntities _context;
        public static HackatonWSREntities Context
        { 
            get
            {
                if (_context == null)
                    _context = new HackatonWSREntities();

                return _context;
            }
        }
    }
}

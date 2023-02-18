using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEyesFast.Data
{
    public partial class SaveEyes2Entities
    {
        private static SaveEyes2Entities _context;
        public static SaveEyes2Entities GetContext()
        {
            if(_context == null )
                _context = new SaveEyes2Entities();

            return _context;
        }
    }
}

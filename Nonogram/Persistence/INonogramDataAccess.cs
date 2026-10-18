using System;
using System.Collections.Generic;
using System.Text;

namespace Nonogram.Persistence
{
    public interface INonogramDataAccess
    {
        public NonogramTable Load(String path);

        public void Save(String path, NonogramTable table);
    }
}

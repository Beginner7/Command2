using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class MoveVariantsResponse : Response
    {
        public List<string> Cells;
        //набор ячеек, которыми можно пойти
    }
}

using System.Collections.Generic;

namespace Protocol
{
    public class MoveVariantsResponse : Response
    {
        public List<string> Cells;
        //набор ячеек, которыми можно пойти
    }
}

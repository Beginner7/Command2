//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChessServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class game
    {
        public int id { get; set; }
        public string playerWhite { get; set; }
        public string playerBlack { get; set; }
        public System.DateTime timeCreateGame { get; set; }
        public Nullable<System.DateTime> timeStartGame { get; set; }
        public int act { get; set; }
        public int turn { get; set; }
        public string eatedWhites { get; set; }
        public string eatedBlacks { get; set; }
    }
}

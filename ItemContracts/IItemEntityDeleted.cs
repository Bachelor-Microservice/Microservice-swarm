using System;
using System.Collections.Generic;
using System.Text;

namespace ItemContracts
{
    public interface IItemEntityDeleted
    {
        int Id { get; set; }
        string ItemNo { get; set; }
    }
}

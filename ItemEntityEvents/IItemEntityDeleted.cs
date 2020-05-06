using System;
using System.Collections.Generic;
using System.Text;

namespace ItemEntityEvents
{
    public interface IItemEntityDeleted
    {
        int Id { get; set; }
        string ItemNo { get; set; }
    }
}

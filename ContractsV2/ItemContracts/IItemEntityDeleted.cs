using System;
using System.Collections.Generic;
using System.Text;

namespace ContractsV2.ItemContracts
{
    public interface IItemEntityDeleted
    {
        string ItemNo { get; set; }
        int Id { get; set; }
    }
}

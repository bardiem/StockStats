using System;

namespace StockStats.Domain
{
    public interface IHaveDateCreated
    {
        DateTime DateCreated { get; set; }
    }
}

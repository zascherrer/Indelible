using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indelible
{
    public static class ContractReceiptDictionary
    {
        public static Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt> ContractReceipts = new Dictionary<string, Nethereum.RPC.Eth.DTOs.TransactionReceipt>();
    }
}
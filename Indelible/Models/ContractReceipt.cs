using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Nethereum.Web3;

namespace Indelible.Models
{
    public class ContractReceipt
    {
        [Key]
        public int Id { get; set; }
        public string DocumentHash { get; set; }
        public string ContractAddress { get; set; }
    }
}
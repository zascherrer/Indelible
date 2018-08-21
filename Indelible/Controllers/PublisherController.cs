using Indelible.Models;
using Ipfs;
using System.IO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3;
using System.Net.Http;
using Nethereum.HdWallet;
using System.Numerics;
using Nethereum.Hex.HexTypes;

namespace Indelible.Controllers
{
    public class PublisherController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Web3 web3;

        // GET: Publisher
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            List<Document> documents = db.Documents.Where(d => d.UserId == userId).ToList();

            return View(documents);
        }

        public ActionResult Upload()
        {
            DocumentFile documentFile = new DocumentFile();

            return View(documentFile);
        }

        [HttpPost]
        public async Task<ActionResult> Upload([Bind(Include =("Id,Title"))] Document document, HttpPostedFileBase file)
        {
            Document newDocument = new Document { Id = document.Id, Title = document.Title };

            string filepath = System.IO.Path.GetFullPath(file.FileName);
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(filepath));
            IpfsStream fileIpfs = new IpfsStream(filepath, mStream);
            IpfsClient ipfs = new IpfsClient();
            MerkleNode node = await ipfs.Add(fileIpfs);

            newDocument.Hash = node.Hash.ToString();
            newDocument.UserId = User.Identity.GetUserId();
            newDocument.UserName = db.Users.Where(u => u.Id == newDocument.UserId).FirstOrDefault().UserName;

            db.Documents.Add(newDocument);
            ContractReceipt receipt = await AddContractToEthereum(newDocument.Hash);

            db.SaveChanges();

            return View();
        }

        public ActionResult Details(int id)
        {
            Document document = db.Documents.Find(id);

            return View(document);
        }

        public async Task<ActionResult> Download(int id)
        {
            return View();
        }

        public async Task<ContractReceipt> AddContractToEthereum(string hash)
        {
            var abi = @"[{""constant"":false,""inputs"":[],""name"":""getHash"",""outputs"":[{""name"":""x"",""type"":""string""}],""type"":""function""},{""constant"":false,""inputs"":[{""name"":""x"",""type"":""string""}],""name"":""sendHash"",""outputs"":[],""type"":""function""},{""inputs"":[],""type"":""constructor""}]";
            var byte_code = "60606040525b5b610282806100146000396000f360606040526000357c010000000000000000000000000000000000000000000000000000000090048063d13319c414610044578063dfb29935146100bf57610042565b005b61005160048050506101c6565b60405180806020018281038252838181518152602001915080519060200190808383829060006004602084601f0104600302600f01f150905090810190601f1680156100b15780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6101136004808035906020019082018035906020019191908080601f016020809104026020016040519081016040528093929190818152602001838380828437820191505050505050909091905050610115565b005b8060006000509080519060200190828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061016457805160ff1916838001178555610195565b82800160010185558215610195579182015b82811115610194578251826000505591602001919060010190610176565b5b5090506101c091906101a2565b808211156101bc57600081815060009055506001016101a2565b5090565b50505b50565b602060405190810160405280600081526020015060006000508054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156102735780601f1061024857610100808354040283529160200191610273565b820191906000526020600020905b81548152906001019060200180831161025657829003601f168201915b5050505050905061027f565b9056";

            //var privateKey = "D689F929EFEBC845B0DE07D0DD21ACD62D415D87DF15809669346055D5889EFB";
            var senderAddress = "0xf99E4906e40C02e79fefd7ad3A8b2BB764c71a17";
            var wordlist = "palace demise dose erupt valve absurd alert pottery initial myself raven dad";

            var account = new Wallet(wordlist, null).GetAccount(senderAddress);

            var web3 = new Web3(account, "https://rinkeby.infura.io/v3/d08e99e349ad4e89baaae55f343694ab");

            var receipt = await web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byte_code, senderAddress, new Nethereum.Hex.HexTypes.HexBigInteger(900000), null);

            ContractReceipt contractReceipt = new ContractReceipt() { DocumentHash = hash, ContractAddress = receipt.ContractAddress };
            db.ContractReceipts.Add(contractReceipt);
            db.SaveChanges();

            var contract = web3.Eth.GetContract(abi, contractReceipt.ContractAddress);

            var sendHashFunction = contract.GetFunction("sendHash");
            var gas = sendHashFunction.EstimateGasAsync(hash);
            web3.TransactionManager.DefaultGas = new HexBigInteger(1000000);

            var receiptFirstSend = await sendHashFunction.SendTransactionAndWaitForReceiptAsync(senderAddress, null, hash);

            return contractReceipt;
        }

        public void AssignWeb3(Web3 web3)
        {
            this.web3 = web3;
        }

        
    }
}
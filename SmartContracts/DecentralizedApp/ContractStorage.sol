contract HashStorage {
 string ipfsHash;
 
 function HashStorage() public {

 }

 function sendHash(string x) public {
   ipfsHash = x;
 }

 function getHash() public returns (string x) {
   return ipfsHash;
 }
}
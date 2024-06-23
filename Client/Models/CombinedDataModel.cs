namespace Client.Models
{
    public class CombinedDataModel
    {
        public List<AccountModel> AccountService { get; set; }
        public List<TransactionModel> TransactionService { get; set; }

        public string Name {  get; set; }
    }

}

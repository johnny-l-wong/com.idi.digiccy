namespace IDI.Digiccy.Models.Transaction
{
    public class TranInfo
    {
        public TranQueue Queue { get; set; } = new TranQueue();

        public TranDetail Detail { get; set; } = new TranDetail();
    }
}

namespace SaleShop.Data.Infrastructure
{
    //Unit of work pattern
    //Đảm bảo cho nhiều thao tác trên 1 giao dịch phải toàn vẹn => phải thành công , giống transaction
    public interface IUnitOfWork
    {
        void Commit();
    }
}
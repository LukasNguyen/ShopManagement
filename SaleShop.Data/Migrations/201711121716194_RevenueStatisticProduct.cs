namespace SaleShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevenueStatisticProduct : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
                p => new
                {
                    fromDate = p.String(),
                    toDate = p.String()
                }
                , 
                @"select o.CreatedDate as Date, sum(od.Price*od.Quantity) as Revenues ,sum((od.Price*od.Quantity)-(p.OriginalPrice*od.Quantity)) as Benefit  from dbo.Orders o
                inner join dbo.OrderDetails od
                on o.ID = od.OrderID
                inner join dbo.Products p
                on od.ProductID = p.ID
                where o.CreatedDate >= cast(@fromDate as date) and o.CreatedDate <= cast(@toDate as date)
                group by o.CreatedDate");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}

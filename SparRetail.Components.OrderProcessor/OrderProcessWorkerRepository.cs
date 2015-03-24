using SparRetail.Core.Config;
using SparRetail.Core.Database;
using SparRetail.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace SparRetail.Components.OrderProcessor
{
    public class OrderProcessWorkerRepository : RepositoryBase, IOrderProcessorWorkerRepository
    {
        public OrderProcessWorkerRepository(IDatabaseConfigCollection config)
            : base(config)
        {

        }

        public int[] InsertOrders(OrderBasket basket, List<OrderBasketItem> items, string retailerConfigKey, string supplierConfigKey)
        {
            /* 
            * Get order basket
            * Get order basket items
            * 
            * Insert supplier order
            * Insert supplier items
            * Insert retailer order
            * Insert retailer items
            * Delete basket and items
            *              
            * Send email to supplier
            */
            int[] results = new int[2];
            IDbTransaction supplierTransaction = null;
            IDbTransaction retailerTransaction = null;
            using (SqlConnection supplierConnection = new SqlConnection(GetConfig(supplierConfigKey).ConnectionString))
            {
                try
                {
                    supplierConnection.Open();
                    supplierTransaction = supplierConnection.BeginTransaction();
                    results[1] = supplierConnection.Query<int>("usp_InsertSupplierOrder", new { 
                        @RetailerId = basket.RetailerId, 
                        @SupplierId = basket.SupplierId,
                        @UserId = basket.UserId,
                        @OrderDate = DateTime.Now
                    }, supplierTransaction, true, GetConfig(supplierConfigKey).CommandTimeout, CommandType.StoredProcedure).FirstOrDefault();

                    items.ForEach(x => 
                    {
                        supplierConnection.Execute("usp_InsertSupplierOrderItem", new {
                            @SupplierOrderId = results[1],
	                        @ProductId = x.ProductId,
	                        @ProductCode = x.ProductCode,
	                        @BarCode = x.BarCode,
	                        @UnitOfMeasure = x.UnitOfMeasure,
	                        @NumberOfUnits = x.NumberOfUnits,
	                        @PricePerUnit = x.PricePerUnit,
	                        @TotalPrice = x.TotalPrice
                        }, supplierTransaction, GetConfig(supplierConfigKey).CommandTimeout, CommandType.StoredProcedure);
                    });
                    

                    try
                    {
                        using (SqlConnection retailerConnection = new SqlConnection(GetConfig(retailerConfigKey).ConnectionString))
                        {
                            retailerConnection.Open();
                            retailerTransaction = retailerConnection.BeginTransaction();
                            results[0] = retailerConnection.Query<int>("usp_InsertRetailerOrder", new {
                                @RetailerId = basket.RetailerId,
                                @SupplierId = basket.SupplierId,
                                @UserId = basket.UserId,
                                @OrderDate = DateTime.Now
                            }, 
                            retailerTransaction, true, GetConfig(retailerConfigKey).CommandTimeout, CommandType.StoredProcedure).FirstOrDefault();

                            items.ForEach(x => { 
                                retailerConnection.Execute("usp_InsertRetailerOrderItem", new {
                                	@OrderId = results[0],
	                                @ProductId = x.ProductId, 
	                                @ProductName = x.ProductName, 
	                                @ProductCode = x.ProductCode, 
	                                @BarCode = x.BarCode, 
	                                @UnitOfMeasure = x.UnitOfMeasure, 
	                                @NumberOfUnits = x.NumberOfUnits, 
	                                @PricePerUnit = x.PricePerUnit, 
	                                @TotalPrice = x.TotalPrice
                                },
                                retailerTransaction, GetConfig(retailerConfigKey).CommandTimeout, CommandType.StoredProcedure);
                            });

                            retailerConnection.Execute("usp_DeleteOrderBasketWithItems", new { @RetailerOrderBasketId = basket.OrderBasketId }, retailerTransaction, GetConfig(retailerConfigKey).CommandTimeout, CommandType.StoredProcedure);

                            retailerTransaction.Commit();
                        }
                        supplierTransaction.Commit();
                    }
                    catch
                    {
                        if (retailerTransaction != null) retailerTransaction.Rollback();
                        throw;
                    }
                    return results;
                }
                catch
                {
                    if (supplierTransaction != null) supplierTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}

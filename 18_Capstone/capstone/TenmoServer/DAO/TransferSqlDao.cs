using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {
        private readonly string connectionString;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Transfer GetTransfer(int transferId)
        {
            Transfer returnTransfer = new Transfer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnTransfer = GetTransferFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfer;
        }

        public List<Transfer> GetOwnTransfers(int userId)
        {
            List<Transfer> returnTransfers = new List<Transfer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE account_from IN(SELECT account_id FROM account WHERE user_id = @user_id) OR " +
                        "account_to IN(SELECT account_id FROM account WHERE user_id = @user_id); ", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = GetTransferFromReader(reader);
                        returnTransfers.Add(transfer);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfers;
        }

        public Transfer AddTransfer(Transfer transferToAdd)
        {
            Transfer returnTransfer = new Transfer();
            int transferId;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand("INSERT INTO transfer (transfer_status_id, transfer_type_id, account_from, account_to, amount) " +
                        "VALUES (@transfer_status_id, @transfer_type_id, @account_from, @account_to, @amount);", conn);
                    // might need to include transfer_id param
                    //OUTPUT INSERTED.transfer_id
                    cmd.Parameters.AddWithValue("@transfer_status_id", transferToAdd.TransferStatusId);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transferToAdd.TransferTypeId);
                    cmd.Parameters.AddWithValue("@account_from", transferToAdd.AccountFrom);
                    cmd.Parameters.AddWithValue("@account_to", transferToAdd.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transferToAdd.Amount);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    transferId = Convert.ToInt32(cmd.ExecuteScalar());

                    //cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id");
                    //cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    //SqlDataReader reader = cmd.ExecuteReader();
                    //if (reader.Read())
                    //{
                    //    returnTransfer = GetTransferFromReader(reader);
                    //}
                }
            }
            catch (SqlException)
            {
                throw;
            }


            return GetTransfer(transferId);
        }

        

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer t = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
            };

            return t;
        }
    }
}

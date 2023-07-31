using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FastFoodDemo.Models
{
    internal class NEWUSER
    {
        DB mydb = new DB();
        public bool insertUser(string ID, string username, string password, string email)
        {
            SqlCommand command = new SqlCommand("INSERT INTO user_login (Id, username, password, email) VALUES (@ID, @us, @pw, @em)", mydb.getConnection);


            command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = ID;
            command.Parameters.Add("@us", SqlDbType.NVarChar).Value = username;
            command.Parameters.Add("@pw", SqlDbType.NVarChar).Value = password;
            command.Parameters.Add("@em", SqlDbType.NVarChar).Value = email;
            

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))

            {
                mydb.closeConnection();
                return true;
            }

            else
            {
                mydb.closeConnection();
                return false;
            }


        }


        public bool checkUsernameExists(string username)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM user_login WHERE username=@usn", db.getConnection);

            command.Parameters.Add("@usn", SqlDbType.VarChar).Value = username;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            // Check if a row with the given username exists in the table
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool insertCustomer(string fullName, string Phone,  string Address, MemoryStream pic)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Customer (userID, Fullname, Phone, Adress, Picture) VALUES (@id, @Fu, @ph, @ad, @pi)", mydb.getConnection);

            command.Parameters.Add("@id", SqlDbType.VarChar).Value = global.userID;
            command.Parameters.Add("@ph", SqlDbType.VarChar).Value = Phone;
            command.Parameters.Add("@Fu", SqlDbType.VarChar).Value = fullName;
            command.Parameters.Add("@ad", SqlDbType.VarChar).Value = Address;
            command.Parameters.Add("@pi", SqlDbType.Image).Value = pic.ToArray();

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))

            {
                mydb.closeConnection();
                return true;
            }

            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool checkCustomerExists(string userID)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE userID = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = userID;

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))

            {
                mydb.closeConnection();
                return true;
            }

            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool updateCustomer(string fullName, string phone, string address, MemoryStream pic)
        {
            SqlCommand command = new SqlCommand("UPDATE Customer SET Fullname = @Fu, Phone = @ph, Adress = @ad, Picture = @pi WHERE userID = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = global.userID;
            command.Parameters.Add("@ph", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@Fu", SqlDbType.VarChar).Value = fullName;
            command.Parameters.Add("@ad", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pi", SqlDbType.Image).Value = pic.ToArray();

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))

            {
                mydb.closeConnection();
                return true;
            }

            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public Customer LoadCustomerInfo(string userID)
        {
            SqlCommand command = new SqlCommand("SELECT Fullname, Phone, Adress, Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            command.Parameters.Add("@us", SqlDbType.VarChar).Value = userID;

            SqlDataReader reader = command.ExecuteReader();

            Customer customer = null;

            if (reader.Read())
            {
                string fullname = reader["Fullname"].ToString();
                string phone = reader["Phone"].ToString();
                string address = reader["Adress"].ToString();
                byte[] pictureBytes = (byte[])reader["Picture"];

                customer = new Customer(fullname, phone, address, pictureBytes);
            }

            reader.Close();

            return customer;
        }


        public class Customer
        {
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public byte[] PictureBytes { get; set; }

            public Customer(string fullName, string phone, string address, byte[] pictureBytes)
            {
                FullName = fullName;
                Phone = phone;
                Address = address;
                PictureBytes = pictureBytes;
            }
        }

        public DataTable getItems(SqlCommand command)
        {
            mydb.openConnection();
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
            mydb.closeConnection();
            
            
        }

        public bool DeleteItems(string name)
        {
            mydb.openConnection();
                SqlCommand command = new SqlCommand("DELETE FROM budget WHERE Name = @name", mydb.getConnection);
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;

                
                int rowsAffected = command.ExecuteNonQuery();
                

                return rowsAffected > 0;
            mydb.closeConnection();
            
        }

        public bool insertItems(string name, string price, MemoryStream picture)
        {
            mydb.openConnection();
                SqlCommand command = new SqlCommand("INSERT INTO budget (Name, Price, Picture, userID)" + " VALUES(@name, @price, @pic, @userId)", mydb.getConnection);
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@price", SqlDbType.VarChar).Value = price;
                command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
                command.Parameters.Add("@userId", SqlDbType.VarChar).Value = global.userID;

                
                int rowsAffected = command.ExecuteNonQuery();
                

                return rowsAffected > 0;
            mydb.closeConnection();
        }


        public bool updateItems(string name, string price, MemoryStream picture)
        {
            mydb.openConnection();
            SqlCommand command = new SqlCommand("UPDATE budget SET Price = @price, picture = @pic WHERE Name = @name", mydb.getConnection);
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@price", SqlDbType.VarChar).Value = price;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
            command.Parameters.Add("@userId", SqlDbType.VarChar).Value = global.userID;


            int rowsAffected = command.ExecuteNonQuery();


            return rowsAffected > 0;
            mydb.closeConnection();
        }

    }
}

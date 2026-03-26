using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography.X509Certificates;

namespace How_To_Use_DataBase_In_C_
{
    internal class Program
    {
        static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=sa123456;"; // Replace with your actual connection string

       
       

       static public bool FindContactsByID(int ContactID,ref stContact ContactInfo)
        {
            bool IsFound=false;

            SqlConnection connaction=new SqlConnection(connectionString);
            string quary = "SELECT * FROM Contacts WHERE ContactID=@ContactID";
            SqlCommand command = new SqlCommand(quary, connaction);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connaction.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    IsFound = true;
                    ContactInfo.ID = (int)reader["ContactID"];
                    ContactInfo.FirstName = (string)reader["FirstName"];
                    ContactInfo.LastName = (string)reader["LastName"];
                    ContactInfo.Email = (string)reader["Email"];
                    ContactInfo.Phone = (string)reader["Phone"];
                    ContactInfo.Address = (string)reader["Address"];
                    ContactInfo.CountryID = (int)reader["CountryID"];








                }else
                {
                    IsFound = false;
                }
                connaction.Close() ;
                reader.Close();

            }catch (Exception ex)
            {
                string E = "Error" + ex;
            }
            return IsFound;
           }


        static  void DeleteRecord(string ContactID)
        {

            SqlConnection connaction=new SqlConnection(connectionString);

            string quary = @"Delete Contacts where ContactID in ("+ContactID+")";
          
 
            SqlCommand command=new SqlCommand(quary, connaction);
            
           
            try
            {
                connaction.Open();
                int rowsEffacted=command.ExecuteNonQuery();

                if(rowsEffacted>0)
                {
                    Console.WriteLine($"Deleted succufully ContactID{ContactID}");
                }else
                {
                    Console.WriteLine("Filed delete Contact");
                }







            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);  
            }
            
            
            connaction.Close();






        }

     
 public struct stContact
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }
            public string Address { get; set; }

            public int CountryID { get; set; }
        }
        static  void Main(string[] args)
        {
            stContact ContactInfo = new stContact();

            
           
            
            DeleteRecord("12,11,8");
          






            Console.ReadKey();
        }
    }
}



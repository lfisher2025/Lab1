using System.Data.SqlClient;
using Lab1.Pages.Data_Classes;


namespace Lab1.Pages.DB
{
    public class DBClass
    {
        // Use this class to define methods that make connecting to
        // and retrieving data from the DB easier.

        // Connection Object at Data Field Level
        public static SqlConnection Lab1DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? Lab1DBConnString =
            "Server=localhost;Database=Lab1;Trusted_Connection=True";



        //Connection Methods:

        //Add a new user

        public static void AddUser(String firstName, String lastName, String middleInitial, String role, String email)
        {
            string addUserQuery = "INSERT INTO USERS (firstName, lastName, middleInitial, role, phoneNumber, email)" +
                " VALUES (" + firstName + ", " + lastName + ", " + middleInitial + ", " + role + ", " + email + ");";
            SqlCommand cmdAddUser = new SqlCommand();
            cmdAddUser.Connection = Lab1DBConnection;
            cmdAddUser.Connection.ConnectionString = Lab1DBConnString;
            cmdAddUser.CommandText = addUserQuery;
            Lab1DBConnection.Open();


        }

        public static void AddEmployee()
        {

        }
        public static void Addfaculty()
        {

        }

        public static void AddRepresentative() 
        {

        }

        public static void AddAdmin()
        {

        }

        public static SqlDataReader ViewAllGrants()
        {
            string GrantSelectString = "SELECT Name FROM GRANT;";
            SqlCommand cmdViewGrants = new SqlCommand();
            cmdViewGrants.Connection = Lab1DBConnection;
            cmdViewGrants.Connection.ConnectionString = Lab1DBConnString;
            cmdViewGrants.CommandText = GrantSelectString; 
            Lab1DBConnection.Open();

            SqlDataReader tempReader = cmdViewGrants.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader ViewUserMessages(int UserID)
        {
            string MessageSelectString = "SELECT Content FROM Message WHERE senderID = " + UserID + ";";
            SqlCommand cmdViewMessage = new SqlCommand();
            cmdViewMessage.Connection = Lab1DBConnection;
            cmdViewMessage.Connection.ConnectionString = Lab1DBConnString;
            cmdViewMessage.CommandText = MessageSelectString;
            Lab1DBConnection.Open();

            SqlDataReader tempreader = cmdViewMessage.ExecuteReader();
            return tempreader;
        }
       
        public static void AddGrant(Grant NewGrant)
        {
            string AddGrantString = "INSERT INTO GRANT (name, amount, businessid) VALUES (" + NewGrant.Name + "," + NewGrant.Amount + "," + NewGrant.BusinessID + ");";
            SqlCommand cmdAddGrant = new SqlCommand();
            cmdAddGrant.Connection = Lab1DBConnection;
            cmdAddGrant.Connection.ConnectionString = Lab1DBConnString;
            cmdAddGrant.CommandText = AddGrantString;
            Lab1DBConnection.Open();
        }



    }
}

using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
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
            string GrantSelectString = "SELECT grantID, name FROM Grants;";
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

        public static void AddBusinessPartner(BusinessPartner NewBusinessPartner)
        {
            string AddPartnerString = "INSERT INTO BusinessPartner (name, representativeID ,Status) VALUES(" + NewBusinessPartner.name + "," + NewBusinessPartner.representativeID
                + "," + NewBusinessPartner.status + ");";
            SqlCommand cmdAddPartner = new SqlCommand();
            cmdAddPartner.Connection = Lab1DBConnection;
            cmdAddPartner.Connection.ConnectionString = Lab1DBConnString;
            cmdAddPartner.CommandText = AddPartnerString;
            Lab1DBConnection.Open();

        }

        public static SqlDataReader ViewAdminProjects(int AdminID)
        {
            string ViewAdminProjectsString = "SELECT Project.name, Grants.amount , Project.dueDate " +
                "FROM Project JOIN Grants ON Project.grantID = Grants.grantID" +
                " WHERE Project.adminID = " + AdminID + ";";

            SqlCommand cmdViewAdminProjects = new SqlCommand();
            cmdViewAdminProjects.Connection = Lab1DBConnection; 
            cmdViewAdminProjects.Connection.ConnectionString = Lab1DBConnString;
            cmdViewAdminProjects.CommandText = ViewAdminProjectsString;
            Lab1DBConnection.Open();

            SqlDataReader tempReader = cmdViewAdminProjects.ExecuteReader();
            return tempReader;

        }

        //public static SqlDataReader ViewProject(int ProjectID)
        //{
        //    string viewProjectString = "SELECT * FROM PROJECT
        //}

        public static void AddNewProject(Project project)
        {
            string getFacultyString = "SELECT facultyID FROM FacultyGrant WHERE grantID = " + project.grantID + ";";
            SqlCommand cmdGetFaculty = new SqlCommand();
            cmdGetFaculty.Connection = Lab1DBConnection;
            cmdGetFaculty.Connection.ConnectionString = Lab1DBConnString;
            cmdGetFaculty .CommandText = getFacultyString;
            Lab1DBConnection.Open();

            SqlDataReader facultyReader = cmdGetFaculty.ExecuteReader();

            int result = 0;
            if (facultyReader.Read())
            {
                result = Convert.ToInt32(facultyReader["facultyID"]);
            }
            Lab1DBConnection.Close();

            string AddProjectString = "INSERT INTO PROJECT (grantID, employeeID, adminID, facultyID, name, dueDate) VALUES (" +
                project.grantID + "," + project.employeeID + "," + "12" + "," + result + "," + project.name + "," + project.DueDate + ");";

            SqlCommand cmdAddProject = new SqlCommand();
            cmdAddProject.Connection = Lab1DBConnection;
            cmdAddProject.Connection.ConnectionString = Lab1DBConnString;
            cmdAddProject.CommandText = AddProjectString;
            Lab1DBConnection.Open();

          
        }
    }
}

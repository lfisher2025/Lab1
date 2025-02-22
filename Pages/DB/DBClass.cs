using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Lab1.Pages.Data_Classes;


namespace Lab1.Pages.DB
{
    public class DBClass
    {
        public string UserID { get; set; }
        // Use this class to define methods that make connecting to
        // and retrieving data from the DB easier.

        // Connection Object at Data Field Level
        public static SqlConnection Lab1DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? Lab1DBConnString =
            "Server=localhost;Database=Lab1;Trusted_Connection=True";



        //Connection Methods:

        //Add a new user

        public static void AddUser(User newUser, int CurrentUserID)
        {
            string addUserQuery = "INSERT INTO Users (firstName, lastName, middleInitial, role, phoneNumber, email) " +
               "VALUES ('" + newUser.FirstName + "', '" + newUser.LastName + "', '" +
               newUser.MiddleInitial + "', '" + newUser.UserType + "', '" +
               newUser.PhoneNumber + "', '" + newUser.Email + "');"; ;
            SqlCommand cmdAddUser = new SqlCommand();
            cmdAddUser.Connection = Lab1DBConnection;
            cmdAddUser.Connection.ConnectionString = Lab1DBConnString;
            cmdAddUser.CommandText = addUserQuery;
            Lab1DBConnection.Open();
            int rowsAffected = cmdAddUser.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();

            string getNewUserID = "SELECT userID FROM Users WHERE firstName = " + "'" + newUser.FirstName + "'" + ";";
            SqlCommand cmdGetNewUserID = new SqlCommand();
            cmdGetNewUserID.Connection = Lab1DBConnection;
            cmdGetNewUserID.Connection.ConnectionString = Lab1DBConnString;
            cmdGetNewUserID.CommandText = getNewUserID;
            Lab1DBConnection.Open();

            SqlDataReader IDreader = cmdGetNewUserID.ExecuteReader();

            int IDResult = 0;
            if (IDreader.Read())
            {
                IDResult = Convert.ToInt32(IDreader["userID"]);
            }
            Lab1DBConnection.Close();

            if (newUser.UserType == "employee")
            {
                DBClass.AddEmployee(IDResult, CurrentUserID);
            }
            else if (newUser.UserType == "admin")
            {
                DBClass.AddAdmin(IDResult);
            }
            else if(newUser.UserType == "representative")
            {
                DBClass.AddRepresentative(IDResult);
            }
            else if( newUser.UserType == "faculty")
            {
                //will be added later, the initial form needs to dynamically change when a faculty user is selected to add new fields
            }


        }

        public static void AddEmployee(int IDResult, int CurrentUserID)
        { 
            string addEmplyString = "INSERT INTO Employee (employeeID, adminID) VALUES ("+ IDResult + "," + CurrentUserID + ");";
            SqlCommand cmdAddEmployee = new SqlCommand();
            cmdAddEmployee.Connection = Lab1DBConnection;
            cmdAddEmployee.Connection.ConnectionString = Lab1DBConnString;
            cmdAddEmployee.CommandText = addEmplyString;
            Lab1DBConnection.Open();
            int rowsAffected = cmdAddEmployee.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();
        }
        public static void Addfaculty(int IDResult)
        {
            //will be added later, the initial form needs to dynamically change when a faculty user is selected to add new fields
        }

        public static void AddRepresentative(int IDResult) 
        {
            string addRepString = "INSERT INTO Representative (representativeID) VALUES (" + IDResult + ");";
            SqlCommand cmdAddRep = new SqlCommand();
            cmdAddRep.Connection = Lab1DBConnection;
            cmdAddRep.Connection.ConnectionString = Lab1DBConnString;
            cmdAddRep.CommandText = addRepString;
            Lab1DBConnection.Open();
            int rowsAffected = cmdAddRep.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();
        } 

        public static void AddAdmin(int IDResult)
        {
            string addAdminString = "INSERT INTO Admin (adminID) VALUES (" + IDResult + ");";
            SqlCommand cmdAddAdmin = new SqlCommand();
            cmdAddAdmin.Connection = Lab1DBConnection;
            cmdAddAdmin.Connection.ConnectionString = Lab1DBConnString;
            cmdAddAdmin.CommandText = addAdminString;
            Lab1DBConnection.Open();
            int rowsAffected = cmdAddAdmin.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();
            
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
            int rowsAffected = cmdAddGrant.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();
        }

        public static void AddBusinessPartner(BusinessPartner NewBusinessPartner)
        {
            string AddPartnerString = "INSERT INTO BusinessPartner (name, representativeID, status) VALUES ('" +
            NewBusinessPartner.name + "', " +
            NewBusinessPartner.representativeID + ", '" +
            NewBusinessPartner.status + "');";

            SqlCommand cmdAddPartner = new SqlCommand();
            cmdAddPartner.Connection = Lab1DBConnection;
            cmdAddPartner.Connection.ConnectionString = Lab1DBConnString;
            cmdAddPartner.CommandText = AddPartnerString;
            Lab1DBConnection.Open();


            int rowsAffected = cmdAddPartner.ExecuteNonQuery(); // Ensures execution

            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            


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

        public static void AddNewProject(Project project, int currentUserID)
        {
            string getFacultyString = "SELECT facultyID FROM FacultyGrant WHERE grantID = " + project.grantID + ";";
            SqlCommand cmdGetFaculty = new SqlCommand();
            cmdGetFaculty.Connection = Lab1DBConnection;
            cmdGetFaculty.Connection.ConnectionString = Lab1DBConnString;
            cmdGetFaculty .CommandText = getFacultyString;
            Lab1DBConnection.Open();

            SqlDataReader facultyReader = cmdGetFaculty.ExecuteReader();

            int facultyResult = 0;
            if (facultyReader.Read())
            {
                facultyResult = Convert.ToInt32(facultyReader["facultyID"]);
            }
            Lab1DBConnection.Close();

            string AddProjectString = "INSERT INTO PROJECT (grantID, employeeID, adminID, facultyID, name, dueDate) VALUES (" +
            project.grantID + ", " +
            project.employeeID + ", " +
            currentUserID + ", " +
            facultyResult + ", '" +  // String values enclosed in single quotes
            project.name + "', '" +  // Name needs single quotes
            project.DueDate.ToString("yyyy-MM-dd") + "');"; // Date formatted properly

            SqlCommand cmdAddProject = new SqlCommand();
            cmdAddProject.Connection = Lab1DBConnection;
            cmdAddProject.Connection.ConnectionString = Lab1DBConnString;
            cmdAddProject.CommandText = AddProjectString;
            Lab1DBConnection.Open();
            int rowsAffected = cmdAddProject.ExecuteNonQuery();


            if (rowsAffected > 0)
            {
                Console.WriteLine("Data inserted successfully.");
            }
            else
            {
                Console.WriteLine("No rows were inserted.");
            }

            Lab1DBConnection.Close();

        }

        public static  SqlDataReader ViewFacultyProjects(int facultyID)
        {
            string ViewEmplyProjString = "SELECT * FROM PROJECT WHERE facultyID = " + facultyID + ";";
            SqlCommand cmdViewEmplyProj = new SqlCommand();
            cmdViewEmplyProj.Connection = Lab1DBConnection; 
            cmdViewEmplyProj.Connection.ConnectionString = Lab1DBConnString;
            cmdViewEmplyProj.CommandText = ViewEmplyProjString;
            Lab1DBConnection.Open();

            SqlDataReader tempReader = cmdViewEmplyProj.ExecuteReader();
            return tempReader;

            Lab1DBConnection.Close();
        }
    }
}

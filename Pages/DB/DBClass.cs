﻿using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Identity;
using Lab1.Pages.DataClasses;
using Lab1.Pages.Data_Classes;
using System.Data;


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

        private static readonly String? AuthConnString =
            "Server=Localhost;Database=AUTH;Trusted_Connection=True";



        //Connection Methods:

        //Add a new user

        public static int AddUser(User newUser, int CurrentUserID)
        {


            //This method and the SQL procedure were developed with the help of ChatGPT.The tool was utilized to assist us with seamlessly linking
            // the generated userID in the Lab1 DB to the UserID in the AUTH DB.Will add this functionality later
            SqlCommand cmdAddUser = new SqlCommand();
            cmdAddUser.Connection = Lab1DBConnection;
            cmdAddUser.Connection.ConnectionString = Lab1DBConnString;
            cmdAddUser.CommandType = CommandType.StoredProcedure;
            cmdAddUser.CommandText = "InsertUserAndGetID";

            // Add input parameters
            cmdAddUser.Parameters.AddWithValue("@FirstName", newUser.FirstName);
            cmdAddUser.Parameters.AddWithValue("@LastName", newUser.LastName);
            cmdAddUser.Parameters.AddWithValue("@MiddleInitial", (object)newUser.MiddleInitial ?? DBNull.Value);
            cmdAddUser.Parameters.AddWithValue("@Role", newUser.UserType);
            cmdAddUser.Parameters.AddWithValue("@PhoneNumber", newUser.PhoneNumber);
            cmdAddUser.Parameters.AddWithValue("@Email", newUser.Email);
            cmdAddUser.Connection.Open();

            // Add output parameter for UserID
            SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmdAddUser.Parameters.Add(outputIdParam);

            int rowsAffected = cmdAddUser.ExecuteNonQuery(); // Execute procedure
            int newUserId = -1; // Initialize a new variable to capture the userID autoincremented by the DB

            if (rowsAffected > 0)
            {
                newUserId = (int)outputIdParam.Value; // Retrieve UserID
                Console.WriteLine($"User inserted successfully. New User ID: {newUserId}");

                return newUserId;

            }
            else
            {
                Console.WriteLine("No rows were inserted.");
                return -1;
            }

            Lab1DBConnection.Close();


            if (newUser.UserType == "employee")
            {
                DBClass.AddEmployee(newUserId, CurrentUserID);
            }
            else if (newUser.UserType == "representative")
            {
                DBClass.AddRepresentative(newUserId);
            }
            else if (newUser.UserType == "faculty")
            {
                //will be added later, the initial form needs to dynamically change when a faculty user is selected to add new fields
            }


        }

        public static void AddEmployee(int IDResult, int CurrentUserID)
        {
            string addEmplyString = "INSERT INTO Employee (employeeID, adminID) VALUES (" + IDResult + "," + CurrentUserID + ");";
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

        public static SqlDataReader ViewAdminProjects()
        {
            string ViewAdminProjectsString = "SELECT Project.name, Grants.amount , Project.dueDate " +
                "FROM Project JOIN Grants ON Project.grantID = Grants.grantID ;";


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
            cmdGetFaculty.CommandText = getFacultyString;
            Lab1DBConnection.Open();

            SqlDataReader facultyReader = cmdGetFaculty.ExecuteReader();

            int facultyResult = 0;
            if (facultyReader.Read())
            {
                facultyResult = Convert.ToInt32(facultyReader["facultyID"]);
            }
            Lab1DBConnection.Close();

            string AddProjectString = "INSERT INTO PROJECT (grantID, employeeID, adminID, facultyID, name, dueDate) " +
                          "OUTPUT INSERTED.ProjectID VALUES (@grantID, @employeeID, @adminID, @facultyID, @name, @dueDate)";
            SqlCommand cmdAddProject = new SqlCommand();
            cmdAddProject.Connection = Lab1DBConnection;
            cmdAddProject.Connection.ConnectionString = Lab1DBConnString;
            cmdAddProject.CommandText = AddProjectString;
  
            cmdAddProject.Parameters.AddWithValue("@grantID", project.grantID);
            cmdAddProject.Parameters.AddWithValue("@employeeID", project.employeeID);
            cmdAddProject.Parameters.AddWithValue("@adminID", currentUserID);
            cmdAddProject.Parameters.AddWithValue("@facultyID", facultyResult);
            cmdAddProject.Parameters.AddWithValue("@name", project.name);
            cmdAddProject.Parameters.AddWithValue("@dueDate", project.DueDate.ToString("yyyy-MM-dd"));

            Lab1DBConnection.Open();
            int newProjectID = (int)cmdAddProject.ExecuteScalar();

            Lab1DBConnection.Close();

            string NoteInsertQuery = "INSERT INTO Note (ProjectID, note) VALUES (@ProjectID, @note);";
            SqlCommand cmdAddNote = new SqlCommand();
            cmdAddNote.Connection = Lab1DBConnection;
            cmdAddNote.Connection.ConnectionString = Lab1DBConnString;
            cmdAddNote.CommandText = NoteInsertQuery;

            cmdAddNote.Parameters.AddWithValue("@ProjectID",newProjectID);
            cmdAddNote.Parameters.AddWithValue("@note", project.note);

            Lab1DBConnection.Open();
            cmdAddNote.ExecuteNonQuery();


        }

        public static SqlDataReader ViewFacultyProjects(int facultyID)
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

        public static bool HashedParameterLogin(string Username, string Password)
        {
            string loginQuery =
                "SELECT Password FROM HashedCredentials WHERE Username = @Username";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab1DBConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);

            cmdLogin.Connection.Open();


            SqlDataReader hashReader = cmdLogin.ExecuteReader();
            if (hashReader.Read())
            {
                string correctHash = hashReader["Password"].ToString();

                if (correctHash == Password) // PasswordHash.ValidatePassword(Password, correctHash) added when hashing later
                {
                    return true;
                }
            }

            return false;
        }




        public static void CreateHashedUser(string Username, string Password, int UserID)
        {
            //The int UserID is the userID generated when the user is added to the Lab1DB. it is retrieved in the AddUser() method
            //This will link the login information with a user in the system.
            string loginQuery =
                "INSERT INTO HashedCredentials (Username,Password,UserID) values (@Username, @Password, @UserID)";

            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = Lab1DBConnection;
            cmdLogin.Connection.ConnectionString = AuthConnString;

            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", Password); //PasswordHash.HashPassword(Password) When hashing
            cmdLogin.Parameters.AddWithValue("@UserID", UserID);

            cmdLogin.Connection.Open();


            cmdLogin.ExecuteNonQuery();

        }

        public static int GetUserID(string username)
        {
            //This method is called after a successful login to add the userID to the session state to be used for subsequent queries. 
            string getIDQuery = "SELECT UserID FROM HashedCredentials WHERE username = @username";

            SqlCommand cmdGetID = new SqlCommand();
            cmdGetID.Connection = Lab1DBConnection;
            cmdGetID.Connection.ConnectionString = AuthConnString;

            cmdGetID.CommandText = getIDQuery;
            cmdGetID.Parameters.AddWithValue("@username", username);

            cmdGetID.Connection.Open();

            int UserID = (int)cmdGetID.ExecuteScalar();

            return UserID;
        }

        public static SqlDataReader GetUserMessages(string UserID)
        {
            string getMessagesQuery = @"
                SELECT 
                    M.messageID,
                    S.firstName + ' ' + S.lastName AS sender,
                    R.firstName + ' ' + R.lastName AS recipient,
                    M.title,
                    M.content,
                    M.readStatus,
                    M.timestamp
                FROM Message M
                JOIN Users S ON M.senderID = S.userID
                JOIN Users R ON M.recipientID = R.userID
                WHERE M.recipientID = @UserID OR M.senderID = @UserID"; ;

            SqlCommand cmdGetMessages = new SqlCommand();
            cmdGetMessages.Connection = Lab1DBConnection;
            cmdGetMessages.Connection.ConnectionString = Lab1DBConnString;
            cmdGetMessages.CommandText = getMessagesQuery;
            cmdGetMessages.Parameters.AddWithValue("@UserID", UserID);
            cmdGetMessages.Connection.Open();

            SqlDataReader tempreader = cmdGetMessages.ExecuteReader();
            return tempreader;
            
        }

        public static int NewMessage(int senderID, int RecipientID, string Title, string Content)
        {
            string newMessageQuery = @"
                INSERT INTO Message (senderID, recipientID, title, content, readStatus, timestamp)
                VALUES (@SenderID, @RecipientID, @Title, @Content, 0, @Timestamp)";

            SqlCommand cmdNewMessage = new SqlCommand();
            cmdNewMessage.Connection = Lab1DBConnection;
            cmdNewMessage.Connection.ConnectionString = Lab1DBConnString;
            cmdNewMessage.CommandText = newMessageQuery;

            cmdNewMessage.Parameters.AddWithValue("@SenderID", senderID);
            cmdNewMessage.Parameters.AddWithValue("@RecipientID", RecipientID);
            cmdNewMessage.Parameters.AddWithValue("@Title", Title);
            cmdNewMessage.Parameters.AddWithValue("@Content", Content);
            cmdNewMessage.Parameters.AddWithValue("@Timestamp", DateTime.Now);
            cmdNewMessage.Connection.Open();

            int rowsAffected = cmdNewMessage.ExecuteNonQuery(); // Ensures execution

            return rowsAffected;

        }

        public static SqlDataReader GetUsers()
        {
            string getUsersQuery = "SELECT userID, firstName, lastName FROM Users";

            SqlCommand cmdGetUsers = new SqlCommand();
            cmdGetUsers.Connection = Lab1DBConnection;
            cmdGetUsers.Connection.ConnectionString = Lab1DBConnString;
            cmdGetUsers.CommandText = getUsersQuery;
            cmdGetUsers.Connection.Open();

            SqlDataReader tempreader = cmdGetUsers.ExecuteReader();

            return tempreader;
        }
    }

}

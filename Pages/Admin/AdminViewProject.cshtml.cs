using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class ViewProjectModel : PageModel
    {
        private readonly string connectionString = "Server=localhost;Database=Lab1;Trusted_Connection=True";

        [BindProperty(SupportsGet = true)]
        public string ProjectName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DueDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SubmissionDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CompleteStatus { get; set; }

        public List<ProjectData> Projects { get; set; } = new();

        public void OnGet()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT p.name AS ProjectName, p.dueDate, p.submissionDate, p.completeStatus
                FROM Project p
                WHERE (@ProjectName IS NULL OR p.name LIKE '%' + @ProjectName + '%')
                AND (@DueDate IS NULL OR p.dueDate = @DueDate)
                AND (@SubmissionDate IS NULL OR p.submissionDate = @SubmissionDate)
                AND (@CompleteStatus IS NULL OR p.completeStatus = @CompleteStatus)";

                using (SqlCommand cmd = new(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProjectName", string.IsNullOrEmpty(ProjectName) ? (object)DBNull.Value : ProjectName);
                    cmd.Parameters.AddWithValue("@DueDate", string.IsNullOrEmpty(DueDate) ? (object)DBNull.Value : DueDate);
                    cmd.Parameters.AddWithValue("@SubmissionDate", string.IsNullOrEmpty(SubmissionDate) ? (object)DBNull.Value : SubmissionDate);
                    cmd.Parameters.AddWithValue("@CompleteStatus", string.IsNullOrEmpty(CompleteStatus) ? (object)DBNull.Value : CompleteStatus);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Projects.Add(new ProjectData
                            {
                                Name = reader["ProjectName"].ToString(),
                                DueDate = reader["dueDate"] as DateTime?,
                                SubmissionDate = reader["submissionDate"] as DateTime?,
                                CompleteStatus = (bool)reader["completeStatus"]
                            });
                        }
                    }
                }
            }
        }

        public class ProjectData
        {
            public string Name { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? SubmissionDate { get; set; }
            public bool CompleteStatus { get; set; }
        }
    }
}

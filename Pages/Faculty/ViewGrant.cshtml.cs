using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Faculty
{
    public class ViewGrantModel : PageModel
    {
        private readonly string connectionString = "Server=localhost;Database=Lab1;Trusted_Connection=True";

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? SearchAmount { get; set; }

        public List<GrantData> Grants { get; set; } = new();

        public void OnGet()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();

                string query = @"
                SELECT Name, Category, Amount
                FROM Grants
                WHERE (@SearchName IS NULL OR Name LIKE '%' + @SearchName + '%')
                AND (@SearchCategory IS NULL OR Category LIKE '%' + @SearchCategory + '%')
                AND (@SearchAmount IS NULL OR Amount = @SearchAmount)";

                using (SqlCommand cmd = new(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchName", string.IsNullOrEmpty(SearchName) ? (object)DBNull.Value : SearchName);
                    cmd.Parameters.AddWithValue("@SearchCategory", string.IsNullOrEmpty(SearchCategory) ? (object)DBNull.Value : SearchCategory);
                    cmd.Parameters.AddWithValue("@SearchAmount", SearchAmount ?? (object)DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Grants.Add(new GrantData
                            {
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Amount = Convert.ToDouble(reader["Amount"])
                            });
                        }
                    }
                }
            }
        }

        public class GrantData
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public double Amount { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MovieLib.Model;

namespace RESTMovieService.DBUtil
{
    public class ManageMovies
    {
        private const string connectionString = "Data Source=rasmus-movie-db-server.database.windows.net;Initial Catalog=Rasmus-movie-db;User ID=Rasmus;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string GET_ALL = "Select * from Movie";
        private const string GET_ID = "Select * from Movie WHERE Id = @Id";
        private const string POST = "Insert into Movie(Id, Title, Director, Genre, ReleaseYear, ImdbRating) values(@Id, @Title, @Director, @Genre, @ReleaseYear, @ImdbRating)";
        private const string PUT = "Update Movie set Title = @Title, Director = @Director, Genre = @Genre, ReleaseYear = @ReleaseYear, ImdbRating = @ImdbRating WHERE Id = @Id";
        private const string DEL = "Delete from Movie WHERE Id = @Id";

        public IEnumerable<Movies> Get()
        {
            List<Movies> movies = new List<Movies>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Movies m = ReadNextElement(reader);
                        movies.Add(m);
                    }
                    reader.Close();
                }
                return movies;
            }
        }

        public Movies GetById(int id)
        {
            Movies movie = new Movies();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GET_ID, conn))
                {
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            movie = ReadNextElement(reader);
                        }
                    }
                }

                return movie;
            }
        }

        public void Post(Movies value)
        {
            Movies movie = new Movies();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(POST, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", movie.Id);
                    cmd.Parameters.AddWithValue("@Title", movie.Title);
                    cmd.Parameters.AddWithValue("@Director", movie.Director);
                    cmd.Parameters.AddWithValue("@Genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@ImdbRating", movie.ImdbRating);

                    cmd.ExecuteNonQuery();
                }
            }
                
        }

        public void Put(int id, Movies movie)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(PUT, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", movie.Title);
                    cmd.Parameters.AddWithValue("@Director", movie.Director);
                    cmd.Parameters.AddWithValue("@Genre", movie.Genre);
                    cmd.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
                    cmd.Parameters.AddWithValue("@ImdbRating", movie.ImdbRating);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            Movies movie = GetById(id);

            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(DEL, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Movies ReadNextElement(SqlDataReader reader)
        {
            Movies movie = new Movies();

            movie.Id = reader.GetInt32(0);
            movie.Title = reader.GetString(1);
            movie.Director = reader.GetString(2);
            movie.Genre = reader.GetString(3);
            movie.ReleaseYear = reader.GetInt32(4);
            movie.ImdbRating = reader.GetDouble(5);

            return movie;
        }
    }
}

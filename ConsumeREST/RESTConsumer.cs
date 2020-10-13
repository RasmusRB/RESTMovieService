using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MovieLib.Model;
using Newtonsoft.Json;

namespace ConsumeREST
{
    internal class RESTConsumer
    {

        private static readonly string URI = "https://moviesrestful.azurewebsites.net/api/movies";

        internal async void Start()
        {
            PrintHeader("Getting all movies");
            IList<Movies> movies = await GetAllMoviesAsync();
            foreach (Movies movie in movies)
            {
                Console.WriteLine(movie);
            }

            PrintHeader("Henter movie nr 1");
            try
            {
                Movies movie = await GetMovieById(1);
                Console.WriteLine("movie = " + movie);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }

            PrintHeader("Henter movie nr 2");
            try
            {
                Movies movie = await GetMovieById(2);
                Console.WriteLine("movie = " + movie);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }

            PrintHeader("Creating new movie");
            Movies newMovie = new Movies(101, "Idk", "PJ", "Fantasy", 1999, 4.9);
            await CreateNewMovie(newMovie);
        }

        private async Task<IList<Movies>> GetAllMoviesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Movies> mList = JsonConvert.DeserializeObject<IList<Movies>>(content);

                return mList;
            }
        }

        private async Task<Movies> GetMovieById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(URI + id);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    Movies movie = JsonConvert.DeserializeObject<Movies>(json);

                    return movie;
                }
                throw new KeyNotFoundException($"Fejl  code ={resp.StatusCode} message={await resp.Content.ReadAsStringAsync()}");
            }
        }

        private async Task<Movies> CreateNewMovie(Movies movie)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json");

                HttpResponseMessage resp = await client.PostAsync(URI, content);
                if (resp.IsSuccessStatusCode)
                {
                    return movie;
                }
                throw new ArgumentException("Create failed");
            }
        }

        private void PrintHeader(string txt)
        {
            Console.WriteLine("============");
            Console.WriteLine(txt);
            Console.WriteLine("============");
        }
    }
}
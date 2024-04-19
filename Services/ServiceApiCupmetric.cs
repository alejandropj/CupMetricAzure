using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using CupMetric.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CupMetric.Services
{
    public class ServiceApiCupmetric
    {
        private string UrlApiCupmetric;
        private MediaTypeWithQualityHeaderValue Header;
        //Objeto para recuperar HttpContext
        private IHttpContextAccessor httpContextAccessor;
        public ServiceApiCupmetric(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.UrlApiCupmetric =
                configuration.GetValue<string>("ApiUrls:ApiCupmetric");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetTokenAsync(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/auth/login";
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add
                    (this.Header);
                LoginModel model = new LoginModel
                {
                    UserName = username,
                    Password = password
                };
                string jsonData = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent
                    (jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);
                    string token = keys.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        #region MÉTODOS GENÉRICOS
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region RECETAS
        public async Task<List<Receta>> GetRecetasAsync()
        {
            string request = "data/recetas";
            List<Receta> recetas = await this.CallApiAsync<List<Receta>>(request);
            return recetas;
        }        
        public async Task<List<RecetaIngredienteValoracion>> GetRecetasFormattedAsync()
        {
            string request = "data/recetas/recetasformatted";
            List<RecetaIngredienteValoracion> recetas = 
                await this.CallApiAsync<List<RecetaIngredienteValoracion>>(request);
            return recetas;
        }        
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            string request = "data/recetas/categorias";
            List<Categoria> categorias = 
                await this.CallApiAsync<List<Categoria>>(request);
            return categorias;
        }

        public async Task<RecetaIngredienteValoracion> FindRecetaFormattedAsync(int idReceta)
        {
            string request = "data/recetas/recetaformatted/"+idReceta;
            RecetaIngredienteValoracion receta =
                await this.CallApiAsync<RecetaIngredienteValoracion>(request);
            return receta;
        }
        public async Task<int> CountRecetasAsync()
        {
            string request = "data/recetas/countrecetas";
            int receta =
                await this.CallApiAsync<int>(request);
            return receta;
        }
        public async Task<Receta> FindRecetaByIdAsync(int idReceta)
        {
            string request = "data/recetas/" + idReceta;
            Receta receta =
                await this.CallApiAsync<Receta>(request);
            return receta;
        }
        public async Task CreateRecetaAsync(RecetaIngrediente receta)
        {
            string request = "data/recetas";
            string jsonData = JsonConvert.SerializeObject(receta);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);


                HttpResponseMessage response =
                    await client.PostAsync(request, content);
/*                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();

                }
                else
                {
                    return "Error al crear receta: " + response.ReasonPhrase;
                }*/

            }
        }
        public async Task UpdateRecetaAsync(Receta receta)
        {
            string request = "data/recetas";
            string jsonData = JsonConvert.SerializeObject(receta);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PutAsync(request, content);

            }
        }
        public async Task DeleteRecetaAsync(int IdReceta)
        {
            string request = "data/recetas/"+IdReceta;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);

            }
        }
        public async Task AddVisitRecetaAsync(int idReceta)
        {
            string request = "data/recetas/AddVisit/"+idReceta;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);
                HttpResponseMessage response =
                    await client.PutAsync(request,null);
            }
        }

        public async Task<List<RecetaIngredienteValoracion>> FilterRecetaByCategoriaAsync(int idCategoria)
        {
            string request = "data/recetas/filterreceta/"+idCategoria;
            List<RecetaIngredienteValoracion> categorias = await this.CallApiAsync<List<RecetaIngredienteValoracion>>(request);
            return categorias;
        }


        public async Task<bool> PostValoracionAsync(int idReceta, int idUsuario, int valoracion)
        {
            string request = "data/recetas";
            Valoracion valo = new Valoracion
            {
                IdReceta = idReceta,
                IdUsuario = idUsuario,
                NumValoracion = valoracion
            };
            string jsonData = JsonConvert.SerializeObject(valo);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);

                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        /*        public async Task<List<Ingrediente>> FindIngredientesbyReceta(int idReceta)
                {
                    *//*            var consulta = from datos in this.context.IngredientesReceta
                                               where datos.IdReceta == idReceta
                                               select datos;*//*

                    var ingredientes = from ingReceta in this.context.IngredientesReceta
                                       join ingrediente in this.context.Ingredientes on ingReceta.IdIngrediente equals ingrediente.IdIngrediente
                                       where ingReceta.IdReceta == idReceta
                                       select ingrediente;

                    return await ingredientes.ToListAsync();
                }*/

        #endregion

        #region INGREDIENTES
        public async Task<List<Ingrediente>> GetIngredientesAsync()
        {
            string request = "data/ingredientes";
            List<Ingrediente> ingredientes = await this.CallApiAsync<List<Ingrediente>>(request);
            return ingredientes;
        }
        public async Task<List<Ingrediente>> GetIngredientesMediblesAsync()
        {
            var consulta = from datos in this.context.Ingredientes
                           where datos.Medible == true
                           select datos;
            List<Ingrediente> ingredientes = await consulta.ToListAsync();
            return ingredientes;
        }
        public async Task<int> CountIngredientesAsync()
        {
            return await this.context.Ingredientes.CountAsync();
        }
        public async Task<Ingrediente> FindIngredienteByIdAsync(int idIngrediente)
        {
            var consulta = from datos in this.context.Ingredientes
                           where datos.IdIngrediente == idIngrediente
                           select datos;
            return consulta.AsEnumerable().FirstOrDefault();
        }
        public async Task CreateIngredienteAsync(Ingrediente ingrediente)
        {
            this.context.Ingredientes.Add(ingrediente);
            this.context.SaveChanges();
        }
        public async Task UpdateIngredienteAsync(Ingrediente ingrediente)
        {
            Ingrediente ingredienteOld = await this.FindIngredienteByIdAsync(ingrediente.IdIngrediente);
            ingredienteOld.Nombre = ingrediente.Nombre;
            ingredienteOld.Densidad = ingrediente.Densidad;
            ingredienteOld.Imagen = ingrediente.Imagen;
            ingredienteOld.Almacenamiento = ingrediente.Almacenamiento;
            ingredienteOld.Sustitutivo = ingrediente.Sustitutivo;
            ingredienteOld.Medible = ingrediente.Medible;
            this.context.SaveChanges();
        }
        public async Task DeleteIngredienteAsync(int idIngrediente)
        {
            Ingrediente ingrediente = await this.FindIngredienteByIdAsync(idIngrediente);
            this.context.Ingredientes.Remove(ingrediente);
            this.context.SaveChanges();
        }

        #endregion

        #region USERS

        #endregion

        #region UTENSILIOS

        #endregion
    }
}

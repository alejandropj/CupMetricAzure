using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using CupMetric.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using CupMetric.Helpers;
using ApiCupMetric.Models;

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
        public async Task<string> GetTokenAsync(string email, string password)
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
                    Email = email,
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
        public async Task CreateRecetaAsync(Receta receta, List<int> ingredientes, List<double> cantidades)
        {
            string request = "data/recetas";
            RecetaIngrediente recetaIngrediente = new RecetaIngrediente
            {
                Receta = receta,
                IdIngredientes = ingredientes,
                Cantidad = cantidades
            };
            string jsonData = JsonConvert.SerializeObject(recetaIngrediente);
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
            string request = "data/ingredientes/ingredientesmedibles";
            List<Ingrediente> ingredientes = await this.CallApiAsync<List<Ingrediente>>(request);
            return ingredientes;
        }
        public async Task<int> CountIngredientesAsync()
        {
            string request = "data/ingredientes/countingredientes";
            int receta =
                await this.CallApiAsync<int>(request);
            return receta;
        }
        public async Task<Ingrediente> FindIngredienteByIdAsync(int idIngrediente)
        {
            string request = "data/ingredientes/"+idIngrediente;
            Ingrediente ingrediente = await this.CallApiAsync<Ingrediente>(request);
            return ingrediente;
        }
        public async Task CreateIngredienteAsync(Ingrediente ingrediente)
        {
            string request = "data/ingredientes";
            string jsonData = JsonConvert.SerializeObject(ingrediente);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);


                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task UpdateIngredienteAsync(Ingrediente ingrediente)
        {
            
        }
        public async Task DeleteIngredienteAsync(int idIngrediente)
        {
            
        }

        #endregion

        #region USERS
        public async Task<User> GetUser()
        {
            string request = "api/auth/user";
            User user = await this.CallApiAsync<User>(request);
            return user;
        } 

        public async Task<TokenModel> LoginUserAsync(string email, string password)
        {
            string request = "api/auth/login";
            LoginModel userLogin = new LoginModel
            {
                Email = email,
                Password = password
            };
            string jsonData = JsonConvert.SerializeObject(userLogin);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response =
                    await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    TokenModel model = JsonConvert.DeserializeObject<TokenModel>(responseData);
                    return model;
                }
                else
                {
                    throw new HttpRequestException("Error al iniciar sesión. Código de estado: " + (int)response.StatusCode);
                }
            }
        }
        public async Task<User> RegisterUserAsync(string nombre, string email, string password)
        {
            string request = "data/user";
            UserReg userReg = new UserReg
            {
                Nombre=nombre,
                Email = email,
                Password = password
            };
            string jsonData = JsonConvert.SerializeObject(userReg);
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
                    string responseData = await response.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(responseData);
                    return user;
                }
                else
                {
                    throw new HttpRequestException("Error al iniciar sesión. Código de estado: " + (int)response.StatusCode);
                }
            }
        }
        public bool Valid(User user)
        {
            return true;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            string request = "data/User";
            List<User> users =
                await this.CallApiAsync<List<User>>(request);
            return users;
        }
        public async Task<int> CountUsersAsync()
        {
            string request = "data/user/countusers";
            int receta =
                await this.CallApiAsync<int>(request);
            return receta;
        }
        public async Task<User> FindUserByIdAsync(int IdUser)
        {
            string request = "data/user/"+IdUser;
            User user =
                await this.CallApiAsync<User>(request);
            return user;
        }

        public async Task UpdateUserAsync(int idUser, string nombre, string email, string password)
        {
            
        }
        public async Task DeleteUserAsync(int idUsuario)
        {
            
        }
        #endregion

        #region UTENSILIOS
        public async Task<List<Utensilio>> GetUtensiliosAsync()
        {
            string request = "data/utensilio";
            List<Utensilio> utensilios =
                await this.CallApiAsync<List<Utensilio>>(request);
            return utensilios;
        }
        public async Task<int> CountUtensiliosAsync()
        {
            string request = "data/utensilio/countutensilios";
            int count =
                await this.CallApiAsync<int>(request);
            return count;
        }
        public async Task<Utensilio> FindUtensilioByIdAsync(int idUtensilio)
        {
            string request = "data/utensilio/"+idUtensilio;
            Utensilio utensilio =
                await this.CallApiAsync<Utensilio>(request);
            return utensilio;

        }
        public async Task CreateUtensilioAsync(Utensilio utensilio)
        {
            string request = "data/utensilio";
            string jsonData = JsonConvert.SerializeObject(utensilio);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApiCupmetric);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);


                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task UpdateUtensilioAsync(Utensilio utensilio)
        {
            
        }
        public async Task DeleteUtensilioAsync(int idUtensilio)
        {
            
        }
        #endregion
    }
}

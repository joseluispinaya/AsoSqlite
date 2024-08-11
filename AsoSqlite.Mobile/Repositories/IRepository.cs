using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsoSqlite.Mobile.DTOs;

namespace AsoSqlite.Mobile.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetUsuario<T>(string urlBase, string url, LoginDTO modeld);
        Task<HttpResponseWrapper<T>> Get<T>(string urlBase, string url);
        Task<HttpResponseWrapper<object>> Post<T>(string urlBase, string url, T model);
    }
}

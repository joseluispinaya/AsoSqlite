using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AsoSqlite.Mobile.DataAccess;
using AsoSqlite.Mobile.DTOs;
using AsoSqlite.Mobile.Utilidades;
using AsoSqlite.Mobile.Modelos;
using System.Collections.ObjectModel;
using AsoSqlite.Mobile.Repositories;
using AsoSqlite.Mobile.Views;
using AsoSqlite.Mobile.Models;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class SincronisarViewModel : ObservableObject
    {
        private readonly IRepository _repository;
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<EAfiliadoDTO> listaAfiliados = new ObservableCollection<EAfiliadoDTO>();

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public SincronisarViewModel(EAfiliadoDbContext context, IRepository repository)
        {
            _dbContext = context;
            _repository = repository;
            ObtenerAfiliados();
        }

        public async void ObtenerAfiliados()
        {
            //var lista = await _dbContext.EAfiliados.ToListAsync();

            // Filtrar los afiliados por aquellos que tienen Estado = true
            var lista = await _dbContext.EAfiliados
                                        .Where(a => a.Estado)
                                        .ToListAsync();

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaAfiliados.Add(new EAfiliadoDTO
                    {
                        IdAfiliado = item.IdAfiliado,
                        NroCI = item.NroCI,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Direccion = item.Direccion,
                        Celular = item.Celular,
                        Estado = item.Estado,
                        EAsociacionId = item.EAsociacionId,
                    });
                }
            }
        }

        [RelayCommand]
        private async Task Savere()
        {
            ObtenerAfiliados();
            if (!ListaAfiliados.Any())
            {
                await Shell.Current.DisplayAlert("Error", "No hay Informacion para enviar.", "Ok");
                return;
            }
            LoadingEsVisible = true;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                LoadingEsVisible = false;
                await Shell.Current.DisplayAlert("Error", "Verifique la conexion a Internet", "Ok");
                return;
            }

            var requestList = ListaAfiliados.Select(d => new AfiliadoRequest
            {
                Idasoci = d.EAsociacionId,
                NroCI = d.NroCI,
                Nombres = d.Nombres,
                Apellidos = d.Apellidos,
                Direccion = d.Direccion,
                Celular = d.Celular
            }).ToList();

            string url = "https://asociacion-001-site1.ktempurl.com/";
            var responseHttp = await _repository.Post(url, "api/afiliados/Registrar", requestList);
            if (responseHttp.Error)
            {
                LoadingEsVisible = false;
                var message = await responseHttp.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            foreach (var afiliado in ListaAfiliados)
            {
                var localAfiliado = await _dbContext.EAfiliados.FindAsync(afiliado.IdAfiliado);
                //localAfiliado.Estado = false;
                if (localAfiliado != null)
                {
                    localAfiliado.Estado = false;
                }
            }
            // Guarda los cambios en la base de datos local
            await _dbContext.SaveChangesAsync();

            LoadingEsVisible = false;
            await Shell.Current.DisplayAlert("Exito", "Lista de Afiliados Sincronizado", "Ok");

            // Opcionalmente, recargar la lista después de la sincronización
            ObtenerAfiliados();
        }
    }
}

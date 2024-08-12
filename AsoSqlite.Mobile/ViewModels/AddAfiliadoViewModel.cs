using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AsoSqlite.Mobile.DataAccess;
using AsoSqlite.Mobile.DTOs;
using AsoSqlite.Mobile.Utilidades;
using AsoSqlite.Mobile.Modelos;
using System.Collections.ObjectModel;

namespace AsoSqlite.Mobile.ViewModels
{
    public partial class AddAfiliadoViewModel : ObservableObject, IQueryAttributable
    {
        private readonly EAfiliadoDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<EAsociacionDTO> listaAsociacion = new ObservableCollection<EAsociacionDTO>();
        
        [ObservableProperty]
        private EAsociacionDTO eAsociacionDto = new EAsociacionDTO();

        [ObservableProperty]
        private EAfiliadoDTO eAfiliadoDto = new EAfiliadoDTO();

        [ObservableProperty]
        private string? tituloPagina;

        private int IdAfiliado;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public AddAfiliadoViewModel(EAfiliadoDbContext context)
        {
            _dbContext = context;

            EAfiliadoDto.Estado = true;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = int.Parse(query["id"].ToString());
            IdAfiliado = id;

            if (IdAfiliado == 0)
            {
                TituloPagina = "Nuevo Afiliado";
            }
            else
            {
                TituloPagina = "Editar Afiliado";
                LoadingEsVisible = true;

                await Task.Run(async () =>
                {
                    var encontrado = await _dbContext.EAfiliados
                    .Include(e => e.EAsociacion)
                    .FirstAsync(e => e.IdAfiliado == IdAfiliado);

                    EAfiliadoDto.IdAfiliado = encontrado.IdAfiliado;
                    EAfiliadoDto.NroCI = encontrado.NroCI;
                    EAfiliadoDto.Nombres = encontrado.Nombres;
                    EAfiliadoDto.Apellidos = encontrado.Apellidos;
                    EAfiliadoDto.Direccion = encontrado.Direccion;
                    EAfiliadoDto.Celular = encontrado.Celular;
                    EAfiliadoDto.Estado = encontrado.Estado;
                    EAfiliadoDto.EAsociacionId = encontrado.EAsociacionId;
                    EAfiliadoDto.EAsociacionNombre = encontrado.EAsociacion?.Nombre;

                    MainThread.BeginInvokeOnMainThread(() => { LoadingEsVisible = false; });
                });
            }
            CargarAsociacion();
            //cargo asociaciones
        }

        [RelayCommand]
        private async Task Guardar()
        {
            LoadingEsVisible = true;
            //EAfiliadoMensaje mensaje = new EAfiliadoMensaje();
            EntidadMensaje<EAfiliadoDTO> mensaje = new EntidadMensaje<EAfiliadoDTO>();

            await Task.Run(async () =>
            {
                if (IdAfiliado == 0)
                {
                    var tbEAfiliado = new EAfiliado
                    {
                        //Idasoci = EAfiliadoDto.Idasoci,
                        NroCI = EAfiliadoDto.NroCI,
                        Nombres = EAfiliadoDto.Nombres,
                        Apellidos = EAfiliadoDto.Apellidos,
                        Direccion = EAfiliadoDto.Direccion,
                        Celular = EAfiliadoDto.Celular,
                        Estado = EAfiliadoDto.Estado,
                        EAsociacionId = EAsociacionDto.Idasoci,
                    };

                    _dbContext.EAfiliados.Add(tbEAfiliado);
                    await _dbContext.SaveChangesAsync();

                    EAfiliadoDto.IdAfiliado = tbEAfiliado.IdAfiliado;
                    mensaje = new EntidadMensaje<EAfiliadoDTO>()
                    {
                        EsCrear = true,
                        EntidadDto = EAfiliadoDto
                    };

                }
                else
                {
                    var encontrado = await _dbContext.EAfiliados
                    .Include(e => e.EAsociacion)
                    .FirstAsync(e => e.IdAfiliado == IdAfiliado);

                    //encontrado.Idasoci = EAfiliadoDto.Idasoci;
                    encontrado.NroCI = EAfiliadoDto.NroCI;
                    encontrado.Nombres = EAfiliadoDto.Nombres;
                    encontrado.Apellidos = EAfiliadoDto.Apellidos;
                    encontrado.Direccion = EAfiliadoDto.Direccion;
                    encontrado.Celular = EAfiliadoDto.Celular;
                    encontrado.Estado = EAfiliadoDto.Estado;
                    encontrado.EAsociacionId = EAsociacionDto.Idasoci;

                    await _dbContext.SaveChangesAsync();

                    // Código para actualizar afiliado existente
                    mensaje = new EntidadMensaje<EAfiliadoDTO>()
                    {
                        EsCrear = false,
                        EntidadDto = EAfiliadoDto
                    };
                }

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    LoadingEsVisible = false;
                    WeakReferenceMessenger.Default.Send(new EntidadMensajeria<EAfiliadoDTO>(mensaje));
                    await Shell.Current.Navigation.PopAsync();
                });
            });

        }

        public async void CargarAsociacion()
        {
            var lista = await _dbContext.EAsociaciones.ToListAsync();
            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaAsociacion.Add(new EAsociacionDTO
                    {
                        Idasoci = item.Idasoci,
                        Nombre = item.Nombre,
                        Direccion = item.Direccion,
                        Telefono = item.Telefono,
                        Correo = item.Correo,
                        Activo = item.Activo,
                    });
                }
            }

            if (!string.IsNullOrEmpty(EAfiliadoDto.EAsociacionNombre))
            {
                EAsociacionDto = ListaAsociacion.FirstOrDefault(g => g.Nombre == EAfiliadoDto.EAsociacionNombre)!;
            }
        }
    }
}

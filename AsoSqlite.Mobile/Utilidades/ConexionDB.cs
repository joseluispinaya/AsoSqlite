

namespace AsoSqlite.Mobile.Utilidades
{
    public static class ConexionDB
    {
        public static string DevolverRuta(string nombreBaseDatos)
        {
            string rutaBaseDatos = string.Empty;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                rutaBaseDatos = Path.Combine(rutaBaseDatos, nombreBaseDatos);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "..", "Library", nombreBaseDatos);
            }
            //else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            //{
            //    rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //    rutaBaseDatos = Path.Combine(rutaBaseDatos, nombreBaseDatos);
            //}
            return rutaBaseDatos;
        }
    }
}

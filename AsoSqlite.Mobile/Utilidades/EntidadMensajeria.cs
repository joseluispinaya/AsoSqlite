using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AsoSqlite.Mobile.Utilidades
{
    public class EntidadMensajeria<T> : ValueChangedMessage<EntidadMensaje<T>>
    {
        public EntidadMensajeria(EntidadMensaje<T> value) : base(value)
        {
            
        }
    }
}

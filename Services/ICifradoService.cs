namespace CaesarApi.Services
{
    public interface ICifradoService
    {
        string Cifrar(string mensaje);
        string Descifrar(string mensajeEncriptado);
    }
} 
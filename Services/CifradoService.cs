namespace CaesarApi.Services
{
    public class CifradoService : ICifradoService
    {
        private const int Desplazamiento = 3;

        public string Cifrar(string mensaje)
        {
            if (string.IsNullOrWhiteSpace(mensaje))
                return string.Empty;
            return new string(mensaje.Select(c => CifrarCaracter(c)).ToArray());
        }

        public string Descifrar(string mensajeEncriptado)
        {
            if (string.IsNullOrWhiteSpace(mensajeEncriptado))
                return string.Empty;
            return new string(mensajeEncriptado.Select(c => DescifrarCaracter(c)).ToArray());
        }

        private char CifrarCaracter(char c)
        {
            if (!char.IsLetter(c)) return c;
            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)(((c + Desplazamiento - offset) % 26) + offset);
        }

        private char DescifrarCaracter(char c)
        {
            if (!char.IsLetter(c)) return c;
            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)(((c - Desplazamiento - offset + 26) % 26) + offset);
        }
    }
} 
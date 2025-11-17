using FluentAssertions;

namespace WordWrapper;

public class TestWordWrapper
{
    [Fact]
    public void Cuando_Texto_Es_Vacio_Deberia_Devolver_Vacio()
    {
        var resultado = Wrap(string.Empty, 10);

        resultado.Should().Be(string.Empty);
    }
    
    [Fact]
    public void Cuando_Texto_Mide_Menos_Que_Columna_Deberia_Devolver_El_Mismo_Texto()
    {
        var resultado = Wrap("hola", 10);

        resultado.Should().Be("hola");
    }
    [Fact]
    public void Cuando_Texto_Mide_Igual_Que_Columna_Deberia_Devolver_El_Mismo_Texto()
    {
        var resultado = Wrap("palabra", 7);

        resultado.Should().Be("palabra");
    }
    [Fact]
    public void Cuando_No_Hay_Espacios_Y_Texto_Sobrepasa_Columna_Deberia_Cortar_En_Columna()
    {
        var resultado = Wrap("palabra", 4);

        resultado.Should().Be("pala\nbra");
    }
    [Fact]
    public void Cuando_No_Hay_Espacios_Y_Texto_Muy_Largo_Deberia_Cortar_En_Multiples_Lineas()
    {
        var resultado = Wrap("abcdefghij", 4);

        resultado.Should().Be("abcd\nefgh\nij");
    }
    [Fact]
    public void Cuando_Hay_Espacio_Justo_En_Columna_Deberia_Cortar_En_Ese_Espacio()
    {
        var resultado = Wrap("hola mundo", 4);

        resultado.Should().Be("hola\nmundo");
    }
    public string Wrap(string text, int column)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        if (text.Length <= column)
            return text;

        string resultado = "";
        int inicio = 0;

        while (inicio < text.Length)
        {
            int longitudRestante = text.Length - inicio;
            int longitudLinea;

            if (longitudRestante < column)
                longitudLinea = longitudRestante;
            else
                longitudLinea = column;

            string segmento = text.Substring(inicio, longitudLinea);

            if (resultado.Length > 0)
                resultado += "\n";

            resultado += segmento;

            inicio += longitudLinea;
        }

        return resultado;
    }
}
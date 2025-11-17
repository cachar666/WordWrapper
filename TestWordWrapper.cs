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
    public string Wrap(string text, int column)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        if (text.Length < column)
            return text;

        return string.Empty;
    }
}
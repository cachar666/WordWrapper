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

    public string Wrap(string text, int column)
    {
        return string.Empty;
    }
}
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

    private object Wrap(string empty, int i)
    {
        throw new NotImplementedException();
    }
}
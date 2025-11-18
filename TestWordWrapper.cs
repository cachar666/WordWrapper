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

        resultado.Should().Be("hola\nmund\no");
    }
    [Fact]
    public void Cuando_Hay_Varios_Espacios_Antes_De_Columna_Deberia_Cortar_En_El_Ultimo_Espacio()
    {
        var resultado = Wrap("a un nivel es lo maximo", 7);

        resultado.Should().Be("a un\nnivel\nes lo\nmaximo");
    }
    [Fact]
    public void Cuando_Existe_Espacio_Justo_En_Columna_Y_El_Resto_Debe_Seguir_Envolviendo()
    {
        var resultado = Wrap("hola a todos en el sistema", 5);

        resultado.Should().Be("hola\na\ntodos\nen el\nsiste\nma");
    }
    [Fact]
    public void Cuando_Existe_Espacio_Justo_En_Columna_Deberia_Eliminar_Ese_Espacio_Y_Seguir()
    {
        var resultado = Wrap("hola a todos", 5);

        resultado.Should().Be("hola\na\ntodos");
    }
    [Fact]
    public void Cuando_No_Hay_Espacio_Antes_De_Columna_Deberia_Cortar_Por_Caracter_Aunque_Parta_Palabra()
    {
        // Arrange / Act
        var resultado = Wrap("super", 1);

        // Assert
        resultado.Should().Be("s\nu\np\ne\nr");
    }
    [Fact]
    public void Cuando_Hay_Multiples_Palabras_Deberia_Aplicar_La_Regla_En_Cada_Linea()
    {
        // Arrange / Act
        var resultado = Wrap("nivel es lo maximo", 7);

        // Assert
        resultado.Should().Be("nivel\nes lo\nmaximo");
    }
    [Fact]
    public void a()
    {
        var result = Wrap("", 1);

        result.Should().Be("");
    }
    [Fact]
    public void b()
    {
        var result = Wrap("this", 10);

        result.Should().Be("this");
    }   
    
    [Fact]
    public void c()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    } 
    
    [Fact]
    public void d()
    {
        var result = Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }
    
    [Fact]
    public void e()
    {
        var result = Wrap("word word", 3);

        result.Should().Be("wor\nd\nwor\nd");
    }
    
    [Fact]
    public void f()
    {
        var result = Wrap("word word", 6);

        result.Should().Be("word\nword");
    }    
    
    [Fact]
    public void f2()
    {
        var result = Wrap("word word", 5);

        result.Should().Be("word\nword");
    }
    
    [Fact]
    public void g()
    {
        var result = Wrap("word word word", 6);

        result.Should().Be("word\nword\nword");
    }
    
    [Fact]
    public void h()
    {
        var result = Wrap("word word word", 11);

        result.Should().Be("word word\nword");
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

            // Si lo que queda cabe en una sola línea, lo devolvemos y salimos
            if (longitudRestante <= column)
            {
                string segmentoFinal = text.Substring(inicio, longitudRestante);

                if (resultado.Length > 0)
                    resultado += "\n";

                resultado += segmentoFinal;
                break;
            }

            // Buscar el ÚLTIMO espacio dentro de la ventana [inicio .. inicio + column]
            int posUltimoEspacio = -1;
            int pos = text.IndexOf(' ', inicio);

            while (pos != -1 && (pos - inicio) <= column)
            {
                posUltimoEspacio = pos;
                pos = text.IndexOf(' ', pos + 1);
            }

            string segmento;

            if (posUltimoEspacio != -1)
            {
                // Cortamos en el último espacio antes o justo en la columna
                int longitudLinea = posUltimoEspacio - inicio; // sin incluir el espacio

                segmento = text.Substring(inicio, longitudLinea);
                inicio = posUltimoEspacio + 1; // saltamos el espacio
            }
            else
            {
                // No hay espacio dentro de la ventana: cortamos por carácter en la columna
                int longitudLinea = column;
                segmento = text.Substring(inicio, longitudLinea);
                inicio += longitudLinea;
            }

            if (resultado.Length > 0)
                resultado += "\n";

            resultado += segmento;
        }

        return resultado;
    }
}
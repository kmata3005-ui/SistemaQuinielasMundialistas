using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class PasswordServiceTests
{
    private readonly PasswordService _service = new();

    [Fact]
    public void Hash_DebeCrearHashPBKDF2()
    {
        string resultado = _service.Hash("Prueba123!");
        Assert.NotNull(resultado);
        Assert.StartsWith("PBKDF2$", resultado);
        Assert.NotEqual("Prueba123!", resultado);
    }

    [Fact]
    public void Verify_ConPasswordCorrecto_DebeRetornarTrue()
    {
        string hash = _service.Hash("Prueba123!");
        Assert.True(_service.Verify("Prueba123!", hash));
    }

    [Fact]
    public void Verify_ConPasswordIncorrecto_DebeRetornarFalse()
    {
        string hash = _service.Hash("Prueba123!");
        Assert.False(_service.Verify("Incorrecta123!", hash));
    }

    [Fact]
    public void Verify_ConPasswordV1Correcto_DebeRetornarTrue()
    {
        Assert.True(_service.Verify("12345678", "12345678"));
    }

    [Fact]
    public void Verify_ConPasswordV1Incorrecto_DebeRetornarFalse()
    {
        Assert.False(_service.Verify("incorrecta", "12345678"));
    }
}

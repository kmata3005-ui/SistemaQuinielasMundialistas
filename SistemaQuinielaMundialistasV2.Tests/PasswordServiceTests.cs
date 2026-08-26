using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class PasswordServiceTests
{
    [Fact]
    public void Hash_DebeCrearHashPBKDF2()
    {
        string resultado = PasswordService.Hash("Prueba123!");

        Assert.NotNull(resultado);
        Assert.StartsWith("PBKDF2$", resultado);
        Assert.NotEqual("Prueba123!", resultado);
    }

    [Fact]
    public void Verify_ConPasswordCorrecto_DebeRetornarTrue()
    {
        string hash = PasswordService.Hash("Prueba123!");

        Assert.True(
            PasswordService.Verify("Prueba123!", hash));
    }

    [Fact]
    public void Verify_ConPasswordIncorrecto_DebeRetornarFalse()
    {
        string hash = PasswordService.Hash("Prueba123!");

        Assert.False(
            PasswordService.Verify("Incorrecta123!", hash));
    }

    [Fact]
    public void Verify_ConPasswordV1Correcto_DebeRetornarTrue()
    {
        Assert.True(
            PasswordService.Verify("12345678", "12345678"));
    }

    [Fact]
    public void Verify_ConPasswordV1Incorrecto_DebeRetornarFalse()
    {
        Assert.False(
            PasswordService.Verify("incorrecta", "12345678"));
    }
}
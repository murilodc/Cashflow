using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Criptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        var mock = new Mock<IPasswordEncripter>();

        mock.Setup(passwordEncripter => passwordEncripter.Encrypt(It.IsAny<string>())).Returns("!2304fd9igfdhfdj834");

        return mock.Object;
    }
}

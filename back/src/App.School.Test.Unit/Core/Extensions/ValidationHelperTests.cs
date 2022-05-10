using App.School.Core.Extensions;
using Shouldly;
using Xunit;

namespace App.School.Test.Unit.Core.Extensions
{
    public class ValidationHelperTests
    {
        [Fact]
        public void CpfShouldBeFalse()
        {
            var cpf = "234.566.227-56";
            cpf.IsValidCpf().ShouldBe(false);
        }

        [Fact]
        public void CpfShouldBeTrue()
        {
            var cpf = "025.968.330-26";
            cpf.IsValidCpf().ShouldBe(true);
        }

        [Fact]
        public void EmailShouldBeFalse()
        {
            var email = "dgdgdh";
            email.IsValidEmail().ShouldBe(false);
        }

        [Fact]
        public void EmailShouldBeTrue()
        {
            var email = "xdfgdg@ert.com";
            email.IsValidEmail().ShouldBe(true);
        }
    }
}
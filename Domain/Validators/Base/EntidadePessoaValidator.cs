using FluentValidation;
using System.Text.RegularExpressions;
using Domain.Models.Base;

namespace Domain.Validators.Base
{
    public class EntidadePessoaValidator : AbstractValidator<EntidadePessoa>
    {
        public EntidadePessoaValidator()
        {
            RuleFor(e => e.Id)
                .Must(e => e.GetType().Equals(typeof(Guid)))
                    .WithMessage("O campo Id deve ser um Guid.")
                .NotNull()
                    .WithMessage("O campo Id é obrigatório.");

            RuleFor(e => e.Nome)
                .NotNull()
                    .WithMessage("O campo Nome é obrigatório.")
                .NotEmpty()
                    .WithMessage("O campo Nome não pode estar vazio.")
                .Length(3, 100)
                    .WithMessage("O campo Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(e => e.DocumentoOficial)
                .NotNull()
                    .WithMessage("O campo DocumentoOficial é obrigatório.")
                .NotEmpty()
                    .WithMessage("O campo DocumentoOficial não pode estar vazio.")
                .Matches(@"^\d{11}$|^\d{14}$")
                    .WithMessage("CPF/CNPJ inválido. Use apenas números, com 11 (CPF) ou 14 (CNPJ) dígitos.")
                .Must(ValidarDocumentoOficial)
                    .WithMessage("O campo DocumentoOficial deve ser um CPF ou CNPJ válido.");

            RuleFor(e => e.Telefone)
                .NotNull()
                    .WithMessage("O campo Telefone é obrigatório.")
                .NotEmpty()
                    .WithMessage("O campo Telefone não pode estar vazio.")
                .Matches(@"^\d{10,11}$")
                    .WithMessage("Telefone inválido. Use apenas números, com 10 ou 11 dígitos. E.G.: '14997594523'");
                    
            RuleFor(e => e.Email)
                .NotNull()
                    .WithMessage("O campo Email é obrigatório.")
                .NotEmpty()
                    .WithMessage("O campo Email não pode estar vazio.")
                .EmailAddress()
                    .WithMessage("O campo Email deve ser um endereço de e-mail válido.");
        }

        private bool ValidarDocumentoOficial(string documento)
        {
            if (!string.IsNullOrEmpty(documento))
            {
                documento = Regex.Replace(documento, "[^0-9]", "");

                if (documento.Length.Equals(11))
                    return ValidarCPF(documento);
                else if (documento.Length.Equals(14))
                    return ValidarCNPJ(documento);
                else 
                    return false;
            }

            return false;
        }

        #region [ Validação dos documentos oficiais (CPF/CNPJ) ]
        public static bool ValidarCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int d1 = CalcularDigito(
                cpf.Substring(0, 9), 
                Enumerable.Range(10, 9).Reverse().ToArray()
            );

            int d2 = CalcularDigito(
                cpf.Substring(0, 9) + d1, 
                Enumerable.Range(2, 9).Reverse().Prepend(11).ToArray()
            );

            return cpf.EndsWith($"{d1}{d2}");
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
                return false;

            int d1 = CalcularDigito(cnpj.Substring(0, 12), [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
            int d2 = CalcularDigito(cnpj.Substring(0, 12) + d1, [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);

            return cnpj.EndsWith($"{d1}{d2}");
        }

        private static int CalcularDigito(string numero, int[] pesos)
        {
            int soma = numero.Select((c, i) => (c - '0') * pesos[i]).Sum();
            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }

    #endregion
}
